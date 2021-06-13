using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RateApp.DAL.Entities;
using System;

namespace RateApp.DAL.Data
{
    public class DatabaseSeeder
    {
        public static void Seed(IServiceProvider applicationServices)
        {
            using (IServiceScope serviceScope = applicationServices.CreateScope())
            {
                DatabaseContext context = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
                if (context.Database.EnsureCreated())
                {
                    PasswordHasher<User> hasher = new PasswordHasher<User>();

                    User user = new User()
                    {
                        Id = Guid.NewGuid().ToString("D"),
                        Email = "yavor@test.test",
                        NormalizedEmail = "yavor@test.test".ToUpper(),
                        UserName = "yavor",
                        NormalizedUserName = "yavor".ToUpper(),
                        SecurityStamp = Guid.NewGuid().ToString("D")
                    };

                    user.PasswordHash = hasher.HashPassword(user, "password");

                    IdentityRole identityRole = new IdentityRole()
                    {
                        Id = Guid.NewGuid().ToString("D"),
                        Name = "Admin",
                        NormalizedName = "Admin".ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString("D")
                    };

                    IdentityUserRole<string> identityUserRole = new IdentityUserRole<string>() { RoleId = identityRole.Id, UserId = user.Id };

                    context.Roles.Add(identityRole);
                    context.Users.Add(user);
                    context.UserRoles.Add(identityUserRole);

                    context.SaveChanges();
                }
            }
        }
    }

}
