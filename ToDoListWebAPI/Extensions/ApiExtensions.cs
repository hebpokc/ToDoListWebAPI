using BusinessLogic.LogicServices.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDoListWebAPI.Endpoints;

namespace ToDoListWebAPI.Extensions
{
    /// <summary>
    /// Содержит методы расширения для настройки API-слоя ASP.NET Core приложения.
    /// Включает регистрацию эндпоинтов и настройку аутентификации через JWT.
    /// </summary>
    public static class ApiExtensions
    {
        /// <summary>
        /// Регистрирует все маршруты (эндпоинты) приложения.
        /// Вызывает методы расширения для регистрации эндпоинтов пользователей, категорий, задач и т.д.
        /// </summary>
        /// <param name="app">Построитель маршрутов.</param>
        public static void AddMapedEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapUsersEndpoints();
            app.MapAuthEndpoints();
            app.MapCategoriesEndpoints();
            app.MapExpensesEndpoints();
            app.MapStatusesEndpoints();
            app.MapTasksEndpoints();
        }

        /// <summary>
        /// Настраивает аутентификацию через JWT Bearer и проверку токенов.
        /// Также настраивает извлечение токена из куки.
        /// </summary>
        /// <param name="services">Коллекция служб приложения.</param>
        /// <param name="jwtOptions">Настройки JWT, загруженные из конфигурации.</param>
        /// <remarks>
        /// Токен извлекается из куки с именем "suspicious-cookies".
        /// Для продакшена рекомендуется использовать более безопасное имя куки.
        /// </remarks>
        public static void AddApiAuthentication(this IServiceCollection services, IOptions<JwtOptions> jwtOptions)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["suspicious-cookies"];

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();
        }
    }
}
