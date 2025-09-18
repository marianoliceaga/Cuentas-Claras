using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasClaras.Infrastructure.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<CuentasClarasDbContext>(options =>
                options.UseSqlServer(connectionString, b =>
                {
                    b.MigrationsAssembly(typeof(CuentasClarasDbContext).Assembly.FullName);
                }));

            return services;
        }
    }
}
