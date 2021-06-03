using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RateApp.DAL;
using RateApp.DAL.Abstraction;
using RateApp.DAL.Data;
using RateApp.DAL.Entities;
using RateApp.Services;
using RateApp.WEB.Auth;
using RateApp.WEB.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateApp.WEB
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RateApp.WEB", Version = "v1" });
                c.OperationFilter<UserHeaderFilter>();
            });


            ///
            /// Solely for DI interfaces are not required 
            /// They are needed for loose coupling and Testability
            ///

            // Register Database context with the dependency njection container
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:Default"]));

            //// Register all implementations of IRepository<T> with EF Repository<>
            //services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            // Register all implementations of IRepository<T> with in memory Repository<>
            services.AddTransient(typeof(IRepository<>), typeof(InMemoryRepository<>));


            // Register IUserService with the UserService implementation
            services.AddTransient<IUserService, UserService>();
            // Register IRestaurantService with the RestaurantService implementation
            services.AddTransient<IRestaurantService, RestaurantService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //DatabaseSeeder.Seed(app.ApplicationServices);

            InMemoryDataSeeder.Seed(app.ApplicationServices);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RateApp.WEB v1");
                });
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
