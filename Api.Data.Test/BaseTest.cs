using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Api.Data.Test
{
    public abstract class BaseTest
    {
        public BaseTest()
        {
        }
    }
    public class DbTest : IDisposable
    {
        private string dataBaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";

        public ServiceProvider ServiceProvider { get; set; }
        public DbTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<GameContext>(o => o.UseSqlServer($"Data Source = localhost, 1433; DataBase={dataBaseName}; User Id = sa; Password = admin123@;"),
                ServiceLifetime.Transient);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            using (var context = ServiceProvider.GetService<GameContext>())
            {
                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<GameContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }

}
