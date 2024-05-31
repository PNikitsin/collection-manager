﻿using Collections.Domain.Entities;
using Collections.Domain.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Collections.Infrastructure.Data
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedEssentialsAsync(IApplicationBuilder applicationBuilder)
        {
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                await roleManager.CreateAsync(new IdentityRole(Roles.Administrator.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

                #region developer

                var developer = new ApplicationUser
                {
                    UserName = "pnikitsin@gmail.com",
                    Email = "pnikitsin@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                };

                if (!context!.Users.Any(user => user.UserName == developer.UserName))
                {
                    await userManager.CreateAsync(developer, "Pa$$w0rd");
                    await userManager.AddToRoleAsync(developer, Roles.Administrator.ToString());
                    await userManager.AddToRoleAsync(developer, Roles.User.ToString());
                }

                #endregion

                #region category

                if (!context!.Categories.Any())
                {
                    var categories = new Category[]
                    {
                        new() { Name = "Perfume" },
                        new() { Name = "Alcohol" },
                        new() { Name = "Books" },
                        new() { Name = "Other" }
                    };

                    await context.Categories.AddRangeAsync(categories);
                    await context.SaveChangesAsync();
                }

                #endregion
            }
        }
    }
}