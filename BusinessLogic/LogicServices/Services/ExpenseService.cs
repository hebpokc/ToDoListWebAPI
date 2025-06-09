using DataAccess.DataRepositories.Interfaces;
using DataAccess.Models;

namespace BusinessLogic.LogicServices.Services
{
    /// <summary>
    /// Реализация сервиса для работы с расходами задач.
    /// Содержит бизнес-логику, связанную с управлением расходами.
    /// </summary>
    public class ExpenseService
    {
        private readonly IExpenseRepository _repository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ExpenseService"/>.
        /// </summary>
        /// <param name="repository">Репозиторий для работы с расходами.</param>
        public ExpenseService(IExpenseRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получает расход по его уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор расхода.</param>
        /// <returns>Объект расхода или null, если не найден.</returns>
        public async Task<Expense?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        /// <summary>
        /// Получает все расходы, связанные с указанной задачей.
        /// </summary>
        /// <param name="taskId">Идентификатор задачи.</param>
        /// <returns>Список расходов, связанных с задачей.</returns>
        public async Task<List<Expense>> GetAllByTaskIdAsync(Guid taskId)
        {
            return await _repository.GetAllByTaskIdAsync(taskId);
        }

        /// <summary>
        /// Создаёт новый расход.
        /// </summary>
        /// <param name="amount">Сумма расходов.</param>
        /// <param name="currency">Валюта расходов.</param>
        /// <param name="spentAt">Дата и время совершения расхода.</param>
        /// <param name="taskId">Идентификатор связанной задачи.</param>
        public async Task CreateAsync(decimal amount, string currency, DateTime spentAt, Guid taskId)
        {
            var expense = new Expense
            {
                Amount = amount,
                Currency = currency,
                SpentAt = spentAt,
                TaskId = taskId
            };

            await _repository.CreateAsync(expense);
        }

        /// <summary>
        /// Обновляет существующий расход.
        /// </summary>
        /// <param name="id">Идентификатор расхода.</param>
        /// <param name="amount">Новая сумма расходов.</param>
        /// <param name="currency">Новая валюта.</param>
        /// <param name="spentAt">Новое дата и время совершения расхода.</param>
        public async Task UpdateAsync(Guid id, decimal amount, string currency, DateTime spentAt)
        {
            var expense = new Expense
            {
                Id = id,
                Amount = amount,
                Currency = currency,
                SpentAt = spentAt
            };

            await _repository.UpdateAsync(expense);
        }

        /// <summary>
        /// Удаляет расход по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор расхода.</param>
        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
