using DataAccess.DataRepositories.Interfaces;
using DataAccess.Models;

namespace BusinessLogic.LogicServices.Services
{
    /// <summary>
    /// Реализация сервиса для работы со статусами задач.
    /// Содержит бизнес-логику, связанную с управлением статусами.
    /// </summary>
    public class StatusService
    {
        private readonly IStatusRepository _repository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="StatusService"/>.
        /// </summary>
        /// <param name="repository">Репозиторий для работы со статусами.</param>
        public StatusService(IStatusRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получает статус по его уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор статуса.</param>
        /// <returns>Объект статуса или null, если не найден.</returns>
        public async Task<Status?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        /// <summary>
        /// Получает список всех статусов.
        /// </summary>
        /// <returns>Список статусов.</returns
        public async Task<List<Status>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        /// <summary>
        /// Создаёт новый статус.
        /// </summary>
        /// <param name="isCompleted">Флаг завершённости задачи.</param>
        public async Task CreateAsync(bool isCompleted)
        {
            var status = new Status
            {
                IsCompleted = isCompleted
            };

            await _repository.CreateAsync(status);
        }

        /// <summary>
        /// Обновляет существующий статус.
        /// </summary>
        /// <param name="id">Идентификатор статуса.</param>
        /// <param name="isCompleted">Новое значение флага завершённости задачи.</param>
        public async Task UpdateAsync(Guid id, bool isCompleted)
        {
            var status = new Status
            {
                Id = id,
                IsCompleted = isCompleted
            };

            await _repository.UpdateAsync(status);
        }

        /// <summary>
        /// Удаляет статус по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор статуса.</param>
        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
