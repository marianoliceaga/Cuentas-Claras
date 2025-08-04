using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanDiario.Infrastructure.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PlanDiarioDbContext>
    {
        public PlanDiarioDbContext CreateDbContext(string[] args)
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

            var optionsBuilder = new DbContextOptionsBuilder<PlanDiarioDbContext>()
                .UseSqlServer(connectionString, b =>
                {
                    b.MigrationsAssembly(typeof(PlanDiarioDbContext).Assembly.FullName);
                });

            return new PlanDiarioDbContext(optionsBuilder.Options);
        }
    }

}
