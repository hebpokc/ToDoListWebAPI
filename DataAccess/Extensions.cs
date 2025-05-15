using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class Extensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection servicesCollection)
        {
            servicesCollection.AddDbContext<ApplicationDbContext>(x =>
            {
                x.UseNpgsql("Host:localhost; Database=ToDoList; Username=vadim; Password=1707");
            });

            return servicesCollection;
        }
    }
}
