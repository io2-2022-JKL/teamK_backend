using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VaccinationSystemApi.Data;

namespace VaccinationSystem.Tests.Integration.WebApi
{
    public static class VaccinationSystemDbContextFactory
    {
        private static VaccinationContext _context;

        static VaccinationSystemDbContextFactory()
        {
            var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkSqlite()
            .AddEntityFrameworkProxies()
            .BuildServiceProvider();

            var connectionStringBuilder = new SqliteConnectionStringBuilder
            { DataSource = ":memory:" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            connection.Open();
            var builder = new DbContextOptionsBuilder<VaccinationContext>();

            builder.UseSqlite(connection)
                .UseInternalServiceProvider(serviceProvider);

            var context = new VaccinationContext(builder.Options);
            context.Database.EnsureCreated();

            _context = context;
        }

        public static VaccinationContext Create()
        {
            return _context;
        }
    }
}
