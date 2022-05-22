using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using VaccinationSystemApi;
using VaccinationSystemApi.Data;

namespace VaccinationSystem.Tests.Integration.WebApi
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<VaccinationContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }


                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkSqlite()
                    .AddEntityFrameworkProxies()
                    .BuildServiceProvider();

                var connectionStringBuilder = new SqliteConnectionStringBuilder
                { DataSource = ":memory:" };
                var connectionString = connectionStringBuilder.ToString();
                var connection = new SqliteConnection(connectionString);
                connection.Open();

                // Add a database context (AppDbContext) using an in-memory database for testing.
                services.AddDbContext<VaccinationContext>(options =>
                {
                    options.UseSqlite(connection);
                    options.UseInternalServiceProvider(serviceProvider);
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var appDb = scopedServices.GetRequiredService<VaccinationContext>();

                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    // Ensure the database is created.
                    appDb.Database.EnsureCreated();
                }
            });
        }
    }
}