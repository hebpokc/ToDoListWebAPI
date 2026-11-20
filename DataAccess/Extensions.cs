using DataAccess.DataRepositories.Interfaces;
using DataAccess.DataRepositories.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    /// <summary>
    /// Содержит методы расширения для регистрации слоя доступа к данным в контейнере зависимостей.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Регистрирует репозитории и контекст базы данных в указанной коллекции служб.
        /// </summary>
        /// <param name="servicesCollection">Коллекция служб, в которую добавляются зависимости.</param>
        /// <param name="configuration">Конфигурация приложения для получения строки подключения.</param>
        /// <returns>Обновлённая коллекция служб с зарегистрированными репозиториями и контекстом БД.</returns>
        public static IServiceCollection AddDataAccess(this IServiceCollection servicesCollection, IConfiguration configuration)
        {
            servicesCollection.AddScoped<IUserRepository, UserRepository>();
            servicesCollection.AddScoped<ICategoryRepository, CategoryRepository>();
            servicesCollection.AddScoped<IExpenseRepository, ExpenseRepository>();
            servicesCollection.AddScoped<IStatusRepository, StatusRepository>();
            servicesCollection.AddScoped<ITaskRepository, TaskRepository>();

            servicesCollection.AddDbContext<ApplicationDbContext>(x =>
            {
                x.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            return servicesCollection;
        }
    }
}
