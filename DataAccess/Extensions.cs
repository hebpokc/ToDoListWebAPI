using DataAccess.DataRepositories.Interfaces;
using DataAccess.DataRepositories.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class Extensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection servicesCollection)
        {
            servicesCollection.AddScoped<IUserRepository, UserRepository>();

            servicesCollection.AddDbContext<ApplicationDbContext>(x =>
            {
                x.UseNpgsql("Host:localhost; Database=ToDoList; Username=vadim; Password=1707");
            });

            return servicesCollection;
        }
    }
}
