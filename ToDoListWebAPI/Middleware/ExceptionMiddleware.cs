namespace ToDoListWebAPI.Middleware
{
    /// <summary>
    /// Обработчик глобальных исключений.
    /// Перехватывает все ошибки, возникшие в процессе выполнения запроса.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ExceptionMiddleware"/>.
        /// </summary>
        /// <param name="next">Следующий делегат в pipeline.</param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Выполняет обработку HTTP-запроса и перехватывает исключения.
        /// </summary>
        /// <param name="context">Контекст HTTP-запроса.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(UnauthorizedAccessException ex)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsync(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
