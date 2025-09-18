using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasClaras.Infrastructure.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CuentasClarasDbContext>
    {
        public CuentasClarasDbContext CreateDbContext(string[] args)
        {
            // Adjust path to point to your API project's appsettings.json if needed
            var basePath = Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<CuentasClarasDbContext>()
                .UseSqlServer(connectionString, b =>
                {
                    b.MigrationsAssembly(typeof(CuentasClarasDbContext).Assembly.FullName);
                });

            return new CuentasClarasDbContext(optionsBuilder.Options);
        }
    }

}
