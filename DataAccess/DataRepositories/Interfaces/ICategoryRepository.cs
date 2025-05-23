using DataAccess.Models;

namespace DataAccess.DataRepositories.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с категориями задач.
    /// Предоставляет асинхронные методы для получения, создания, обновления и удаления категорий.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Получает категорию по её уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор категории.</param>
        /// <returns>Объект категории или null, если не найден.</returns>
        Task<Category?> GetByIdAsync(Guid id);

        /// <summary>
        /// Получает список всех категорий.
        /// </summary>
        /// <returns>Список категорий.</returns>
        Task<List<Category>> GetAllAsync();

        /// <summary>
        /// Создаёт новую категорию.
        /// </summary>
        /// <param name="category">Новая категория.</param>
        /// <returns>Созданная категория.</returns>
        Task CreateAsync(Category category);

        /// <summary>
        /// Обновляет существующую категорию.
        /// </summary>
        /// <param name="category">Обновлённые данные категории.</param>
        Task UpdateAsync(Category category);

        /// <summary>
        /// Удаляет категорию по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        Task DeleteAsync(Guid id);
    }
}
