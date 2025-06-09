using BusinessLogic.LogicServices.Services;
using DataAccess.Requests;

namespace ToDoListWebAPI.Endpoints
{
    /// <summary>
    /// Содержит минимальные API-эндпоинты для работы с пользователями.
    /// Предоставляет маршруты для регистрации и входа пользователей.
    /// </summary>
    public static class AuthEndpoints
    {
        /// <summary>
        /// Регистрирует эндпоинты для работы с пользователями.
        /// </summary>
        /// <param name="app">Построитель маршрутов.</param>
        /// <returns>Объект <see cref="IEndpointRouteBuilder"/> после добавления маршрутов.</returns>
        public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);

            app.MapPost("login", Login);

            return app;
        }

        /// <summary>
        /// Обрабатывает запрос на регистрацию нового пользователя.
        /// </summary>
        /// <param name="request">Данные для регистрации пользователя.</param>
        /// <param name="authService">Сервис для работы с пользователями.</param>
        /// <returns>Результат операции: 200 OK при успешной регистрации.</returns>
        private static async Task<IResult> Register(UserRegisterRequest request, AuthService authService)
        {
            await authService.Register(request.Username, request.Email, request.Password);

            return Results.Ok();
        }

        /// <summary>
        /// Обрабатывает запрос на вход пользователя.
        /// </summary>
        /// <param name="request">Данные для входа (email и пароль).</param>
        /// <param name="authService">Сервис для работы с пользователями.</param>
        /// <param name="context">HTTP-контекст для установки кук.</param>
        /// <returns>Результат операции: 200 OK с JWT-токеном или 401 при ошибке.</returns>
        private static async Task<IResult> Login(UserLoginRequest request,
            AuthService authService,
            HttpContext context)
        {
            var token = await authService.Login(request.Email, request.Password);

            context.Response.Cookies.Append("suspicious-cookies", token);

            return Results.Ok();
        }
    }
}
