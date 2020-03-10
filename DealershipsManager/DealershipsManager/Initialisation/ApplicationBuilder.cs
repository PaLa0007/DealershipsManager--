using DealershipsManager.Data;
using DealershipsManager.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealershipsManager.Initialisation
{
    public static class ApplicationBuilder
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<DealershipsManagerDbContext>();
                db.Database.Migrate();

                if (!db.Roles.AnyAsync().Result)
                {
                    var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                    Task.Run(async () =>
                    {
                        var adminRole = GlobalConstants.AdminRole;
                        var userRole = GlobalConstants.UserRole;

                        await roleManager.CreateAsync(new IdentityRole
                        {
                            Name = adminRole
                        });

                        await roleManager.CreateAsync(new IdentityRole
                        {
                            Name = userRole
                        });
                    }).Wait();

                    User user = new User
                    {
                        UserName = "admin",
                        FirstName = "admin",
                        LastName = "admin",
                        PhoneNumber = "admin",
                        PersonalNumber = "admin",
                        Email = "admin@admin.admin",
                        Address = "admin",
                        IsAdministrator = true
                    };

                    var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                    string pass = "admin";
                    Task.Run(async () =>
                    {
                        await userManager.CreateAsync(user, pass);
                        await userManager.AddToRoleAsync(user, "Admin");
                    }).Wait();
                }
            }

            return app;
        }
    }
}
