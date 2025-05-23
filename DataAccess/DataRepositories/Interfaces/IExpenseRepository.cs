using DataAccess.Models;

namespace DataAccess.DataRepositories.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с расходами задач.
    /// Предоставляет асинхронные методы для получения, создания, обновления и удаления расходов.
    /// </summary>
    public interface IExpenseRepository
    {
        /// <summary>
        /// Получает расход по его уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор расхода.</param>
        /// <returns>Объект расхода или null, если не найден.</returns>
        Task<Expense?> GetByIdAsync(Guid id);

        /// <summary>
        /// Получает все расходы, связанные с указанной задачей.
        /// </summary>
        /// <param name="taskId">Идентификатор задачи.</param>
        /// <returns>Список расходов, связанных с задачей.</returns>
        Task<List<Expense>> GetAllByTaskIdAsync(Guid taskId);

        /// <summary>
        /// Создаёт новый расход.
        /// </summary>
        /// <param name="expense">Новый расход.</param>
        Task CreateAsync(Expense expense);

        /// <summary>
        /// Обновляет существующий расход.
        /// </summary>
        /// <param name="expense">Обновлённые данные расхода.</param>
        Task UpdateAsync(Expense expense);

        /// <summary>
        /// Удаляет расход по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор расхода.</param>
        Task DeleteAsync(Guid id);
    }
}
