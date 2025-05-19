using BusinessLogic.LogicServices.Services;
using DataAccess.Models;
using DataAccess.Requests;

namespace ToDoListWebAPI.Endpoints
{
    public static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);

            app.MapPost("login", Login);

            return app;
        }

        private static async Task<IResult> Register(UserRegisterRequest request, UserService userService)
        {
            await userService.Register(request.Username, request.Email, request.Password);

            return Results.Ok();
        }

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
