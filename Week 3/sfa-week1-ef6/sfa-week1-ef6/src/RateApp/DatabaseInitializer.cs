using RateApp.DAL.Data;
using RateApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateApp
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        public override void InitializeDatabase(DatabaseContext context)
        {
            base.InitializeDatabase(context);
        }

        protected override void Seed(DatabaseContext context)
        {
            context.Users.Add(new User() { Name = "yavor" });

            context.SaveChanges();

            base.Seed(context);

        }
    }
}
