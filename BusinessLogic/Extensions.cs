using BusinessLogic.LogicServices.Interfaces.Auth;
using BusinessLogic.LogicServices.Interfaces.Email;
using BusinessLogic.LogicServices.Services;
using BusinessLogic.LogicServices.Services.Auth;
using BusinessLogic.LogicServices.Services.Email;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic
{
    /// <summary>
    /// Содержит методы расширения для регистрации бизнес-логики приложения в контейнере зависимостей.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Регистрирует сервисы бизнес-слоя в указанной коллекции служб.
        /// Включает основные сервисы: пользователи, категории, расходы, статусы, задачи, а также вспомогательные: JWT и хэширование паролей.
        /// </summary>
        /// <param name="servicesCollection">Коллекция служб, в которую добавляются сервисы.</param>
        /// <returns>Обновлённая коллекция служб с зарегистрированными сервисами.</returns>
        public static IServiceCollection AddBusinessLogic(this IServiceCollection servicesCollection)
        {
            servicesCollection.AddScoped<UserService>();
            servicesCollection.AddScoped<AuthService>();
            servicesCollection.AddScoped<CategoryService>();
            servicesCollection.AddScoped<ExpenseService>();
            servicesCollection.AddScoped<StatusService>();
            servicesCollection.AddScoped<TaskService>();
            servicesCollection.AddScoped<IEmailService, EmailService>();
            servicesCollection.AddScoped<IJwtProvider, JwtProvider>();
            servicesCollection.AddScoped<IPasswordHasher, PasswordHasher>();

            return servicesCollection;
        }
    }
}
