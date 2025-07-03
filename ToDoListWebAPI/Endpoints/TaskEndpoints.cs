using BusinessLogic.LogicServices.Services;
using DataAccess.Requests;

namespace ToDoListWebAPI.Endpoints
{
    /// <summary>
    /// Содержит минимальные API-эндпоинты для работы с задачами (To-Do Tasks).
    /// Предоставляет маршруты для создания, получения, обновления и удаления задач.
    /// </summary>
    public static class TaskEndpoints
    {
        /// <summary>
        /// Регистрирует эндпоинты для работы с задачами.
        /// </summary>
        /// <param name="app">Построитель маршрутов.</param>
        /// <returns>Объект <see cref="IEndpointRouteBuilder"/> после добавления маршрутов.</returns>
        public static IEndpointRouteBuilder MapTasksEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/task");

            group.MapPost("/create", Create).RequireAuthorization();
            group.MapGet("/getById/{id:guid}", GetById).RequireAuthorization();
            group.MapGet("/getByUserId/{userId:guid}", GetAllByUserId).RequireAuthorization();
            group.MapPut("/update/{id:guid}", Update).RequireAuthorization();
            group.MapDelete("/delete/{id:guid}", Delete).RequireAuthorization();

            return app;
        }

        /// <summary>
        /// Обрабатывает запрос на создание новой задачи.
        /// </summary>
        /// <param name="request">Данные для создания задачи.</param>
        /// <param name="taskService">Сервис для работы с задачами.</param>
        /// <returns>Результат операции: 200 OK и идентификатор задачи при успешном создании.</returns>
        private static async Task<IResult> Create(
            TaskCreateRequest request,
            TaskService taskService)
        {
            var createdTask = await taskService.CreateAsync(
                request.Title,
                request.Description,
                request.DueDate,
                request.CategoryId,
                request.StatusId,
                request.UserId);

            return Results.Ok(new { id = createdTask.Id });
        }

        /// <summary>
        /// Обрабатывает запрос на получение задачи по ID.
        /// </summary>
        /// <param name="id">Уникальный идентификатор задачи.</param>
        /// <param name="taskService">Сервис для работы с задачами.</param>
        /// <returns>Результат операции: 200 OK с данными задачи или 404, если не найдена.</returns>
        private static async Task<IResult> GetById(
            Guid id,
            TaskService taskService)
        {
            var task = await taskService.GetByIdAsync(id);

            if (task == null)
            {
                return Results.NotFound();
            }

            // В реальности лучше возвращать DTO вместо сущности
            return Results.Ok(task);
        }

        /// <summary>
        /// Обрабатывает запрос на получение всех задач пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="taskService">Сервис для работы с задачами.</param>
        /// <returns>Результат операции: 200 OK со списком задач.</returns>
        private static async Task<IResult> GetAllByUserId(
            Guid userId,
            TaskService taskService)
        {
            var tasks = await taskService.GetAllByUserIdAsync(userId);

            return Results.Ok(tasks);
        }

        /// <summary>
        /// Обрабатывает запрос на обновление существующей задачи.
        /// </summary>
        /// <param name="id">Идентификатор задачи.</param>
        /// <param name="request">Новые данные задачи.</param>
        /// <param name="taskService">Сервис для работы с задачами.</param>
        /// <returns>Результат операции: 200 OK при успехе.</returns>
        private static async Task<IResult> Update(
            Guid id,
            TaskUpdateRequest request,
            TaskService taskService)
        {
            await taskService.UpdateAsync(
                id,
                request.Title,
                request.Description,
                request.DueDate,
                request.CategoryId,
                request.StatusId,
                request.UserId);

            return Results.Ok();
        }

        /// <summary>
        /// Обрабатывает запрос на удаление задачи по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор задачи.</param>
        /// <param name="taskService">Сервис для работы с задачами.</param>
        /// <returns>Результат операции: 200 OK при успехе.</returns>
        private static async Task<IResult> Delete(
            Guid id,
            TaskService taskService)
        {
            await taskService.DeleteAsync(id);

            return Results.Ok();
        }
    }
}
