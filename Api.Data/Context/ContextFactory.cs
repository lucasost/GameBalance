using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Data.Context
{
    class ContextFactory : IDesignTimeDbContextFactory<GameContext>
    {
        public GameContext CreateDbContext(string[] args)
        {
            var connectionString = @"Data Source=localhost,1433;Initial Catalog = Games;User Id=sa;Password=admin123@;";
            //var connectionString = @"Data Source=localhost,1433;Initial Catalog=Games;Integrated Security=True;User Id=sa;Password=admin123@;";
            //var connectionString = @"Data Source = sqlserver,1433; Initial Catalog = Games; User Id=sa;Password=admin123@;"; ;
            var optionsBuilder = new DbContextOptionsBuilder<GameContext>();
            //optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
            return new GameContext(optionsBuilder.Options);
        }
    }
}
