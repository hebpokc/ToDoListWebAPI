using BusinessLogic.LogicServices.Services;
using DataAccess.Models;
using DataAccess.Requests;

namespace ToDoListWebAPI.Endpoints
{
    /// <summary>
    /// Содержит минимальные API-эндпоинты для работы с пользователями.
    /// Предоставляет маршруты для регистрации и входа пользователей.
    /// </summary>
    public static class UserEndpoints
    {
        /// <summary>
        /// Регистрирует эндпоинты для работы с пользователями.
        /// </summary>
        /// <param name="app">Построитель маршрутов.</param>
        /// <returns>Объект <see cref="IEndpointRouteBuilder"/> после добавления маршрутов.</returns>
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);

            app.MapPost("login", Login);

            return app;
        }

        /// <summary>
        /// Обрабатывает запрос на регистрацию нового пользователя.
        /// </summary>
        /// <param name="request">Данные для регистрации пользователя.</param>
        /// <param name="userService">Сервис для работы с пользователями.</param>
        /// <returns>Результат операции: 200 OK при успешной регистрации.</returns>
        private static async Task<IResult> Register(UserRegisterRequest request, UserService userService)
        {
            await userService.Register(request.Username, request.Email, request.Password);

            return Results.Ok();
        }

        /// <summary>
        /// Обрабатывает запрос на вход пользователя.
        /// </summary>
        /// <param name="request">Данные для входа (email и пароль).</param>
        /// <param name="userService">Сервис для работы с пользователями.</param>
        /// <param name="context">HTTP-контекст для установки кук.</param>
        /// <returns>Результат операции: 200 OK с JWT-токеном или 401 при ошибке.</returns>
        private static async Task<IResult> Login(UserLoginRequest request,
            UserService userService,
            HttpContext context)
        {
            var token = await userService.Login(request.Email, request.Password);

            context.Response.Cookies.Append("suspicious-cookies", token);

            return Results.Ok();
        }
    }
}
