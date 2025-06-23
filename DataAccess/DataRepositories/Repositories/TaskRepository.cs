using DataAccess.DataRepositories.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataRepositories.Repositories
{
    /// <summary>
    /// Реализация репозитория для работы с задачами (TaskEntity).
    /// Использует асинхронные методы для создания, чтения, обновления и удаления задач.
    /// </summary>
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TaskRepository"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получает задачу по идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор задачи.</param>
        /// <returns>Задача или null, если не найдена.</returns>
        public async Task<TaskEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Получает все задачи пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Список задач пользователя.</returns>
        public async Task<List<TaskEntity>> GetAllByUserIdAsync(Guid userId)
        {
            return await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        /// <summary>
        /// Создает новую задачу.
        /// </summary>
        /// <param name="task">Новая задача.</param>
        public async Task CreateAsync(TaskEntity task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// Обновляет существующую задачу.
        /// </summary>
        /// <param name="task">Обновленные данные задачи.</param>
        public async Task UpdateAsync(TaskEntity task)
        {
            await _context.Tasks
                .Where(t => t.Id == task.Id)
                .ExecuteUpdateAsync(x => x
                .SetProperty(t => t.Title, task.Title)
                .SetProperty(t => t.Description, task.Description)
                .SetProperty(t => t.DueDate, task.DueDate)
                .SetProperty(t => t.UserId, task.UserId) 
                .SetProperty(t => t.CategoryId, task.CategoryId)
                .SetProperty(t => t.StatusId, task.StatusId));
        }

        /// <summary>
        /// Удаляет задачу по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор задачи.</param>
        public async Task DeleteAsync(Guid id)
        {
            var task = await _context.Tasks
                .Include(t => t.Expense)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task.Expense != null)
            {
                _context.Expenses.Remove(task.Expense);
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
