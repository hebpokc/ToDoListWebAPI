using DataAccess.DataRepositories.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataRepositories.Repositories
{
    /// <summary>
    /// Реализация репозитория для работы с категориями задач.
    /// Использует асинхронные методы для получения, создания, обновления и удаления категорий.
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CategoryRepository"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получает категорию по её уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор категории.</param>
        /// <returns>Объект категории или null, если не найден.</returns>
        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Получает список всех категорий.
        /// </summary>
        /// <returns>Список категорий.</returns>
        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories
                .ToListAsync();
        }

        /// <summary>
        /// Создаёт новую категорию.
        /// </summary>
        /// <param name="category">Новая категория.</param>
        public async Task CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// Обновляет существующую категорию.
        /// </summary>
        /// <param name="category">Обновлённые данные категории.</param>
        public async Task UpdateAsync(Category category)
        {
            {
                await _context.Categories
                    .Where(c => c.Id == category.Id)
                    .ExecuteUpdateAsync(x => x
                        .SetProperty(c => c.Name, category.Name));
            }
        }

        /// <summary>
        /// Удаляет категорию по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        public async Task DeleteAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
