using DataAccess.Models;

namespace DataAccess.DataRepositories.Interfaces
{
    /// <summary>
    /// Интерфейс для работы со статусами задач.
    /// Предоставляет асинхронные методы для получения, создания, обновления и удаления статусов.
    /// </summary>
    public interface IStatusRepository
    {
        /// <summary>
        /// Получает статус по его уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор статуса.</param>
        /// <returns>Объект статуса или null, если не найден.</returns>
        Task<Status?> GetByIdAsync(Guid id);

        /// <summary>
        /// Получает список всех статусов.
        /// </summary>
        /// <returns>Список статусов.</returns>
        Task<List<Status>> GetAllAsync();

        /// <summary>
        /// Создаёт новый статус.
        /// </summary>
        /// <param name="status">Новый статус.</param>
        Task CreateAsync(Status status);

        /// <summary>
        /// Обновляет существующий статус.
        /// </summary>
        /// <param name="status">Обновлённые данные статуса.</param>
        Task UpdateAsync(Status status);

        /// <summary>
        /// Удаляет статус по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор статуса.</param>
        Task DeleteAsync(Guid id);
    }
}
