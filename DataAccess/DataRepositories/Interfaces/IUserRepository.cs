using DataAccess.Models;

namespace DataAccess.DataRepositories.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория для работы с пользователями.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Получает пользователя по ID.
        /// </summary>
        Task<ApplicationUser?> GetByIdAsync(Guid id);

        /// <summary>
        /// Получает пользователя по email.
        /// </summary>
        Task<ApplicationUser?> GetByEmailAsync(string email);

        /// <summary>
        /// Создаёт нового пользователя.
        /// </summary>
        Task<ApplicationUser> CreateAsync(ApplicationUser user);

        /// <summary>
        /// Обновляет существующего пользователя.
        /// </summary>
        Task UpdateAsync(ApplicationUser user);

        /// <summary>
        /// Удаляет пользователя по ID.
        /// </summary>
        Task DeleteAsync(Guid id);
    }
}
