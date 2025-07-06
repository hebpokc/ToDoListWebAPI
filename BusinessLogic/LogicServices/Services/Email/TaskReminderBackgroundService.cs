using BusinessLogic.LogicServices.Interfaces.Email;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.LogicServices.Services.Email
{
    /// <summary>
    /// Фоновый сервис, который каждый час проверяет задачи с приближающимся или просроченным дедлайном и отправляет email-уведомления.
    /// </summary>
    public class TaskReminderBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<TaskReminderBackgroundService> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromHours(1);

        public TaskReminderBackgroundService(IServiceScopeFactory scopeFactory, ILogger<TaskReminderBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("TaskReminderBackgroundService started.");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                    var now = DateTime.UtcNow;
                    var remindThreshold = now.AddHours(24);

                    var tasksToRemind = await dbContext.Tasks
                        .Where(t => !dbContext.TaskReminders.Any(r => r.TaskId == t.Id)
                                    && (t.DueDate <= remindThreshold || t.DueDate < now))
                        .ToListAsync(stoppingToken);

                    foreach (var task in tasksToRemind)
                    {
                        var user = await dbContext.Users.FindAsync(new object[] { task.UserId }, cancellationToken: stoppingToken);
                        if (user == null || string.IsNullOrEmpty(user.Email))
                        {
                            _logger.LogWarning($"User with ID {task.UserId} not found or has no email.");
                            continue;
                        }

                        var subject = $"Напоминание: Задача \"{task.Title}\" скоро истекает или просрочена";

                        var body = $@"
                    <html>
                    <head>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                margin: 0;
                                padding: 0;
                                background-color: #f4f4f4;
                            }}
                            .container {{
                                width: 100%;
                                max-width: 600px;
                                margin: 20px auto;
                                background-color: #ffffff;
                                padding: 20px;
                                border-radius: 8px;
                                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                            }}
                            h2 {{
                                color: #333333;
                                text-align: center;
                            }}
                            p {{
                                color: #555555;
                                line-height: 1.6;
                            }}
                            .task-details {{
                                background-color: #f9f9f9;
                                padding: 10px;
                                border-left: 5px solid #4CAF50;
                                margin-bottom: 20px;
                            }}
                            .task-details p {{
                                margin: 5px 0;
                            }}
                            .footer {{
                                text-align: center;
                                color: #777777;
                                font-size: 12px;
                                margin-top: 20px;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <h2>Напоминание о задаче</h2>
                            <div class='task-details'>
                                <p><strong>Задача:</strong> {task.Title}</p>
                                <p><strong>Описание:</strong> {task.Description}</p>
                                <p><strong>Срок выполнения:</strong> {task.DueDate.ToLocalTime():f}</p>
                            </div>
                            <p>Пожалуйста, проверьте статус задачи и выполните необходимые действия.</p>
                            <p>Если вы уже выполнили задачу, не забудьте обновить её статус в системе.</p>
                            <div class='footer'>
                                <p>Это письмо было отправлено автоматически. Пожалуйста, не отвечайте на него.</p>
                            </div>
                        </div>
                    </body>
                    </html>
                    ";

                        await emailService.SendEmailAsync(user.Email, subject, body);

                        dbContext.TaskReminders.Add(new TaskReminder
                        {
                            Id = Guid.NewGuid(),
                            TaskId = task.Id,
                            SentAt = DateTime.UtcNow
                        });
                    }

                    await dbContext.SaveChangesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка при отправке напоминаний задач.");
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }

            _logger.LogInformation("TaskReminderBackgroundService stopped.");
        }
    }


}
