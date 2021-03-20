using Api.CrossCutting.DependencyInjection;
using Api.Data.Context;
using Api.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameBalance
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            ConfigureService.ConfigureDependenciesService(services);
            ConfigureRepository.ConfigureDependenciesRepository(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers();

            services.AddSwaggerGen(a =>
            {
                a.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                a.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Desafio técnico FULLSTACK",
                    Description = "Arquitetura DDD - TDD",
                    Contact = new OpenApiContact
                    {
                        Name = "Lucas Ost",
                        Email = "lucasost.sm@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/lucasost/")
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(a =>
            {
                a.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio Fullstack");
                a.RoutePrefix = "/swagger";
                a.RoutePrefix = string.Empty;
            });

            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Seed data
            //using (var service = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //{
            //    using (var context = service.ServiceProvider.GetService<GameContext>())
            //    {
            //        context.Database.Migrate();

            //        if (!context.Games.Any())
            //        {
            //            var games = new List<GameEntity>();

            //            for (int i = 0; i < 200; i++)
            //            {
            //                var randomGameId = new Random();
            //                var randomPlayerId = new Random();
            //                var randomWin = new Random();

            //                games.Add(new GameEntity()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    GameId = randomWin.Next(1, 30),
            //                    PlayerId = randomWin.Next(1, 120),
            //                    Win = randomWin.Next(-5, 8),
            //                    Timestamp = DateTime.UtcNow
            //                });
            //            }

            //            context.Games.AddRangeAsync(games);

            //            context.SaveChanges();
            //        }
            //    }
            //}
        }
    }
}
