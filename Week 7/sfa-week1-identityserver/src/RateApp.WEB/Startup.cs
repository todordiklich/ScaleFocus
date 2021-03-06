using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RateApp.BLL.Services;
using RateApp.DAL;
using RateApp.DAL.Abstraction;
using RateApp.DAL.Data;
using RateApp.DAL.Entities;
using RateApp.Services;
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

            #region Register MVC 

            services.AddControllers();

            #endregion

            #region OpenAPI

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RateApp.WEB", Version = "v1" });

                // Adds the authorize button in swagger UI 
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                // Uses the token from the authorize input and sends it as a header
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                            {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            #endregion


            // EF
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:Default"]));

            // EF Identity
            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedEmail = false;
            })
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<DatabaseContext>();

            // DAL
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            // BLL
            services.AddTransient<IUserManager, RateAppUserManager>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRestaurantService, RestaurantService>();

            // Common
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();


            // IdentityServer
            var builder = services.AddIdentityServer((options) =>
                                                    {
                                                        options.EmitStaticAudienceClaim = true;
                                                    })
                                   .AddInMemoryApiScopes(IdentityConfig.ApiScopes)
                                   .AddInMemoryClients(IdentityConfig.Clients);

            builder.AddDeveloperSigningCredential();
            builder.AddResourceOwnerValidator<PasswordValidator>();

            // Authentication
            // Adds the asp.net auth services
            services
                .AddAuthorization()
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })

                // Adds the JWT bearer token services that will authenticate each request based on the token in the Authorize header
                // and configures them to validate the token with the options
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://localhost:5001"; 
                    options.Audience = "https://localhost:5001/resources";
                });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            DatabaseSeeder.Seed(app.ApplicationServices);

            //Adds the Identityserver Middleware that will handle 
            app.UseIdentityServer();

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

            // Adds the auth middlewares
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
