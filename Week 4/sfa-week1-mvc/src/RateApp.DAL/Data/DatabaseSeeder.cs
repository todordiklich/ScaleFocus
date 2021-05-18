using RateApp.DAL.Data;
using RateApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateApp.DAL.Data
{    public class DatabaseSeeder
    {
        public static void Seed(DatabaseContext context)
        {
            if (context.Database.EnsureCreated())
            {
                context.Users.Add(new User() { Name = "yavor" });

                context.SaveChanges();
            }
        }
    }

}
