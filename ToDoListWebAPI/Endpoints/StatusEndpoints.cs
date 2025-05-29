using BusinessLogic.LogicServices.Services;
using DataAccess.Requests;

namespace ToDoListWebAPI.Endpoints
{
    /// <summary>
    /// Содержит минимальные API-эндпоинты для работы со статусами задач.
    /// Предоставляет маршруты для создания, получения, обновления и удаления статусов.
    /// </summary>
    public static class StatusEndpoints
    {
        /// <summary>
        /// Регистрирует эндпоинты для работы со статусами задач.
        /// </summary>
        /// <param name="app">Построитель маршрутов.</param>
        /// <returns>Объект <see cref="IEndpointRouteBuilder"/> после добавления маршрутов.</returns>
        public static IEndpointRouteBuilder MapStatusesEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/status");

            group.MapPost("/create", Create).RequireAuthorization();
            group.MapGet("/getById/{id:guid}", GetById).RequireAuthorization();
            group.MapPut("/update/{id:guid}", Update).RequireAuthorization();
            group.MapDelete("/delete/{id:guid}", Delete).RequireAuthorization();

            return app;
        }

        /// <summary>
        /// Обрабатывает запрос на создание нового статуса.
        /// </summary>
        /// <param name="request">Данные для создания статуса.</param>
        /// <param name="statusService">Сервис для работы со статусами.</param>
        /// <returns>Результат операции: 200 OK при успешном создании.</returns>
        private static async Task<IResult> Create(
            StatusCreateRequest request,
            StatusService statusService)
        {
            await statusService.CreateAsync(request.IsCompleted);

            return Results.Ok();
        }

        /// <summary>
        /// Обрабатывает запрос на получение статуса по ID.
        /// </summary>
        /// <param name="id">Идентификатор статуса.</param>
        /// <param name="statusService">Сервис для работы со статусами.</param>
        /// <returns>Результат операции: 200 OK с данными статуса или 404, если не найден.</returns>
        private static async Task<IResult> GetById(
            Guid id,
            StatusService statusService)
        {
            var status = await statusService.GetByIdAsync(id);

            if (status == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(status);
        }

        /// <summary>
        /// Обрабатывает запрос на обновление существующего статуса.
        /// </summary>
        /// <param name="id">Идентификатор статуса.</param>
        /// <param name="request">Новые данные статуса.</param>
        /// <param name="statusService">Сервис для работы со статусами.</param>
        /// <returns>Результат операции: 200 OK при успехе.</returns>
        private static async Task<IResult> Update(
            Guid id,
            StatusUpdateRequest request,
            StatusService statusService)
        {
            await statusService.UpdateAsync(id, request.IsCompleted);

            return Results.Ok();
        }

        /// <summary>
        /// Обрабатывает запрос на удаление статуса по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор статуса.</param>
        /// <param name="statusService">Сервис для работы со статусами.</param>
        /// <returns>Результат операции: 200 OK при успехе.</returns>
        private static async Task<IResult> Delete(
            Guid id,
            StatusService statusService)
        {
            await statusService.DeleteAsync(id);

            return Results.Ok();
        }
    }
}
