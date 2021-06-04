using Microsoft.Extensions.DependencyInjection;
using RateApp.DAL.Abstraction;
using RateApp.DAL.Entities;
using System;

namespace RateApp.DAL.Data
{
    public class InMemoryDataSeeder
    {
        public static void Seed(IServiceProvider applicationServices)
        {
            using (IServiceScope serviceScope = applicationServices.CreateScope())
            {
                var repo = serviceScope.ServiceProvider.GetRequiredService<IRepository<User>>();
                repo.CreateOrUpdate(new User() { Name = "yavor" });
            }

        }
    }
}