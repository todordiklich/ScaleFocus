using Microsoft.Extensions.DependencyInjection;
using RateApp.DAL.Entities;
using System;

namespace RateApp.DAL.Data
{    public class DatabaseSeeder
    {
        public static void Seed(IServiceProvider applicationServices)
        {
            using (IServiceScope serviceScope = applicationServices.CreateScope())
            {
                DatabaseContext context = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
                if (context.Database.EnsureCreated())
                {
                    context.Users.Add(new User() { Name = "yavor" });

                    context.SaveChanges();
                }
            }
        }
    }

}
