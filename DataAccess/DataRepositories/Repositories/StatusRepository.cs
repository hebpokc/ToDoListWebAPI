using DataAccess.DataRepositories.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataRepositories.Repositories
{
    /// <summary>
    /// Реализация репозитория для работы со статусами задач.
    /// Использует асинхронные методы для получения, создания, обновления и удаления статусов.
    /// </summary>
    public class StatusRepository : IStatusRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="StatusRepository"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public StatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получает статус по его уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор статуса.</param>
        /// <returns>Объект статуса или null, если не найден.</returns>
        public async Task<Status?> GetByIdAsync(Guid id)
        {
            return await _context.Statuses
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        /// <summary>
        /// Получает список всех статусов.
        /// </summary>
        /// <returns>Список статусов.</returns>
        public async Task<List<Status>> GetAllAsync()
        {
            return await _context.Statuses
                .ToListAsync();
        }

        /// <summary>
        /// Создаёт новый статус.
        /// </summary>
        /// <param name="status">Новый статус.</param>
        public async Task CreateAsync(Status status)
        {
            await _context.Statuses.AddAsync(status);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновляет существующий статус.
        /// </summary>
        /// <param name="status">Обновлённые данные статуса.</param>
        public async Task UpdateAsync(Status status)
        {
            await _context.Statuses
                .Where(s => s.Id == status.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(s => s.IsCompleted, status.IsCompleted));
        }

        /// <summary>
        /// Удаляет статус по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор статуса.</param>
        public async Task DeleteAsync(Guid id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status != null)
            {
                _context.Statuses.Remove(status);
                await _context.SaveChangesAsync();
            }
        }
    }
}
