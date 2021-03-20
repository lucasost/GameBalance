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
            var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
            //var connectionString = @"Data Source=localhost,1433;Initial Catalog = Games;User Id=sa;Password=admin123@;";
            var optionsBuilder = new DbContextOptionsBuilder<GameContext>();
            optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
            return new GameContext(optionsBuilder.Options);
        }
    }
}
