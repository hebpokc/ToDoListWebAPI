using BusinessLogic.LogicServices.Interfaces;
using BusinessLogic.LogicServices.Interfaces.Auth;
using BusinessLogic.LogicServices.Services;
using BusinessLogic.LogicServices.Services.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic
{
    public static class Extensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection servicesCollection)
        {
            servicesCollection.AddScoped<UserService>();
            servicesCollection.AddScoped<IJwtProvider, JwtProvider>();
            servicesCollection.AddScoped<IPasswordHasher, PasswordHasher>();

            return servicesCollection;
        }
    }
}
