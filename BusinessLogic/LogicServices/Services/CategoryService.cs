using DataAccess.DataRepositories.Interfaces;
using DataAccess.Models;

namespace BusinessLogic.LogicServices.Services
{
    /// <summary>
    /// Реализация сервиса для работы с категориями задач.
    /// Содержит бизнес-логику, связанную с категориями.
    /// </summary>
    public class CategoryService
    {
        private readonly ICategoryRepository _repository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CategoryService"/>.
        /// </summary>
        /// <param name="repository">Репозиторий для работы с категориями.</param>
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получает категорию по её уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор категории.</param>
        /// <returns>Объект категории или null, если не найден.</returns>
        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        /// <summary>
        /// Получает список всех категорий.
        /// </summary>
        /// <returns>Список категорий.</returns>
        public async Task<List<Category>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        /// <summary>
        /// Создаёт новую категорию.
        /// </summary>
        /// <param name="name">Название категории.</param>
        public async Task CreateAsync(string name)
        {
            var category = new Category
            {
                Name = name
            };

            await _repository.CreateAsync(category);
        }

        /// <summary>
        /// Обновляет существующую категорию.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="name">Новое название категории.</param>
        public async Task UpdateAsync(Guid id, string name)
        {
            var category = new Category
            {
                Id = id,
                Name = name
            };

            await _repository.UpdateAsync(category);
        }

        /// <summary>
        /// Удаляет категорию по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
