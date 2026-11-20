using BusinessLogic.LogicServices.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        /// <param name="configuration">Конфигурация приложения для чтения настроек JWT.</param>
        /// <remarks>
        /// Токен извлекается из куки с именем "todolist-auth".
        /// </remarks>
        public static void AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();

            if (jwtOptions == null || string.IsNullOrEmpty(jwtOptions.SecretKey))
                throw new InvalidOperationException("JWT SecretKey не задан. Убедитесь, что он указан в appsettings.json или user-secrets.");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtOptions.Audience,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["todolist-auth"];

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();
        }
    }
}
