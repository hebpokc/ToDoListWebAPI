using DataAccess.Models;

namespace DataAccess.DataRepositories.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с задачами (TaskEntity).
    /// Предоставляет методы для асинхронного доступа к данным.
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>
        /// Получает задачу по идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор задачи.</param>
        /// <returns>Задача или null, если не найдена.</returns>
        Task<TaskEntity?> GetByIdAsync(Guid id);

        /// <summary>
        /// Получает все задачи пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Список задач пользователя.</returns>
        Task<List<TaskEntity>> GetAllByUserIdAsync(Guid userId);

        /// <summary>
        /// Создает новую задачу.
        /// </summary>
        /// <param name="task">Новая задача.</param>
        Task CreateAsync(TaskEntity task);

        /// <summary>
        /// Обновляет существующую задачу.
        /// </summary>
        /// <param name="task">Обновленные данные задачи.</param>
        Task UpdateAsync(TaskEntity task);

        /// <summary>
        /// Удаляет задачу по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор задачи.</param>
        Task DeleteAsync(Guid id);
    }
}
