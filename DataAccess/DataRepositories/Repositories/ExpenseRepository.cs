using DataAccess.DataRepositories.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataRepositories.Repositories
{
    /// <summary>
    /// Реализация репозитория для работы с расходами задач.
    /// Использует асинхронные методы для получения, создания, обновления и удаления расходов.
    /// </summary>
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ExpenseRepository"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public ExpenseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получает расход по его уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор расхода.</param>
        /// <returns>Объект расхода или null, если не найден.</returns>
        public async Task<Expense?> GetByIdAsync(Guid id)
        {
            return await _context.Expenses
                .Include(e => e.Task)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Получает все расходы, связанные с указанной задачей.
        /// </summary>
        /// <param name="taskId">Идентификатор задачи.</param>
        /// <returns>Список расходов, связанных с задачей.</returns>
        public async Task<List<Expense>> GetAllByTaskIdAsync(Guid taskId)
        {
            return await _context.Expenses
                .Where(e => e.TaskId == taskId)
                .ToListAsync();
        }

        /// <summary>
        /// Создаёт новый расход.
        /// </summary>
        /// <param name="expense">Новый расход.</param>
        public async Task CreateAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновляет существующий расход.
        /// </summary>
        /// <param name="expense">Обновлённые данные расхода.</param>
        public async Task UpdateAsync(Expense expense)
        {
            await _context.Expenses
                .Where(e => e.Id == expense.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(e => e.Amount, expense.Amount)
                    .SetProperty(e => e.Currency, expense.Currency)
                    .SetProperty(e => e.SpentAt, expense.SpentAt));
        }

        /// <summary>
        /// Удаляет расход по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор расхода.</param>
        public async Task DeleteAsync(Guid id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
        }
    }
}
