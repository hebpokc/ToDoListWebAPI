using DataAccess.DataRepositories.Interfaces;
using DataAccess.Models;

namespace BusinessLogic.LogicServices.Services
{
    /// <summary>
    /// Реализация сервиса для работы с задачами.
    /// Содержит бизнес-логику, связанную с управлением задачами.
    /// </summary>
    public class TaskService
    {
        private readonly ITaskRepository _repository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TaskService"/>.
        /// </summary>
        /// <param name="repository">Репозиторий для работы с задачами.</param>
        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получает задачу по её уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор задачи.</param>
        /// <returns>Объект задачи или null, если не найден.</returns>
        public async Task<TaskEntity?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        /// <summary>
        /// Получает все задачи пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Список задач пользователя.</returns>
        public async Task<List<TaskEntity>> GetAllByUserIdAsync(Guid userId)
        {
            return await _repository.GetAllByUserIdAsync(userId);
        }

        /// <summary>
        /// Создаёт новую задачу.
        /// </summary>
        /// <param name="title">Заголовок задачи.</param>
        /// <param name="description">Описание задачи.</param>
        /// <param name="dueDate">Дата завершения задачи.</param>
        /// <param name="categoryId">Идентификатор категории задачи.</param>
        /// <param name="statusId">Идентификатор статуса задачи.</param>
        /// <param name="userId">Идентификатор пользователя, которому принадлежит задача.</param>
        public async Task<TaskEntity> CreateAsync(
            string title,
            string description,
            DateTime dueDate,
            Guid categoryId,
            Guid statusId,
            Guid userId)
        {
            var task = new TaskEntity
            {
                Title = title,
                Description = description,
                DueDate = dueDate,
                CategoryId = categoryId,
                StatusId = statusId,
                UserId = userId
            };

            await _repository.CreateAsync(task);

            return task;
        }

        /// <summary>
        /// Обновляет существующую задачу.
        /// </summary>
        /// <param name="id">Идентификатор задачи для обновления.</param>
        /// <param name="title">Новый заголовок задачи.</param>
        /// <param name="description">Новое описание задачи.</param>
        /// <param name="dueDate">Новая дата завершения задачи.</param>
        /// <param name="categoryId">Новый идентификатор категории задачи.</param>
        /// <param name="statusId">Новый идентификатор статуса задачи.</param>
        public async Task UpdateAsync(
                Guid id,
                string title,
                string description,
                DateTime dueDate,
                Guid categoryId,
                Guid statusId,
                Guid userId)
        {
            var task = new TaskEntity
            {
                Id = id,
                Title = title,
                Description = description,
                DueDate = dueDate,
                CategoryId = categoryId,
                StatusId = statusId,
                UserId = userId
            };

            await _repository.UpdateAsync(task);
        }

        /// <summary>
        /// Удаляет задачу по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор задачи.</param>
        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        /// <summary>
        /// Отмечает задачу как выполненную, изменяя её статус на соответствующий.
        /// </summary>
        /// <param name="taskId">Идентификатор задачи.</param>
        /// <param name="completedStatusId">Идентификатор статуса "выполнено".</param>
        public async Task MarkAsCompletedAsync(Guid taskId, Guid completedStatusId)
        {
            var task = await _repository.GetByIdAsync(taskId);

            if (task == null)
                throw new InvalidOperationException("Задача не найдена.");

            task.StatusId = completedStatusId;

            await _repository.UpdateAsync(task);
        }
    }
}
