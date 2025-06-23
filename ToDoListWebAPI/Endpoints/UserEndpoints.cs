using BusinessLogic.LogicServices.Services;
using DataAccess.Models;
using DataAccess.Requests;

namespace ToDoListWebAPI.Endpoints
{
    /// <summary>
    /// Содержит минимальные API-эндпоинты для работы с пользователями.
    /// Предоставляет маршруты: GET, PUT, DELETE.
    /// </summary>
    public static class UserEndpoints
    {
        /// <summary>
        /// Регистрирует маршруты для работы с пользователями.
        /// </summary>
        /// <param name="app">Построитель маршрутов.</param>
        /// <returns>Объект <see cref="IEndpointRouteBuilder"/> после добавления маршрутов.</returns>
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/user");

            group.MapGet("/getById/{id:guid}", GetById);
            group.MapPut("/update/{id:guid}", Update);
            group.MapDelete("/delete/{id:guid}", Delete);

            return app;
        }

        /// <summary>
        /// Получает пользователя по ID.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="userService">Сервис для работы с пользователями.</param>
        /// <returns>200 OK с данными пользователя или 404 Not Found.</returns>
        private static async Task<IResult> GetById(
            Guid id,
            UserService userService)
        {
            var user = await userService.GetByIdAsync(id);

            if (user == null)
            {
                return Results.NotFound(new { Message = "User not found" });
            }

            return Results.Ok(user);
        }

        /// <summary>
        /// Обновляет данные пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="request">Новые данные пользователя.</param>
        /// <param name="userService">Сервис для работы с пользователями.</param>
        /// <returns>200 OK при успешном обновлении.</returns>
        private static async Task<IResult> Update(
            Guid id,
            UserUpdateRequest request,
            UserService userService)
        {
            if (request is null)
            {
                return Results.BadRequest("Request data is required.");
            }

            await userService.UpdateAsync(id, request);

            return Results.Ok();
        }

        /// <summary>
        /// Удаляет пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="userService">Сервис для работы с пользователями.</param>
        /// <returns>200 OK при успешном удалении.</returns>
        private static async Task<IResult> Delete(
            Guid id,
            UserService userService)
        {
            await userService.DeleteAsync(id);

            return Results.Ok();
        }
    }
}
