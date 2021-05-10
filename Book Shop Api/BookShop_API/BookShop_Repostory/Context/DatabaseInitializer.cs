using BookShop_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop_API.BookShop_Repostory.Context
{
    internal class DatabaseInitializer
    {

        internal static void Seed(IServiceScope scoped)
        {
            using (var _context = scoped.ServiceProvider.GetRequiredService<IdentityAppContext>())
            {

                var manager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                if (!manager.Users.Any())
                {
                    var configuration = scoped.ServiceProvider.GetRequiredService<IConfiguration>();
                    AppUser user = new AppUser
                    {
                        UserName = configuration["User:username"],
                        Email = configuration["User:email"],
                        Name= configuration["User:name"],
                        Surname= configuration["User:surname"],
                        BirthDate =DateTime.Now
                        
                    };
                    manager.CreateAsync(user, configuration["User:password"]).GetAwaiter().GetResult();

                    var roles = scoped.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                    List<Role> list = new List<Role>();

                    if (!roles.Roles.Any())
                    {

                        string[] UserRoles = configuration["Roles"].Split(",");
                        foreach (var role in UserRoles)
                        {
                            Role identityRole = new Role
                            {
                                Name = role
                            };
                            list.Add(identityRole);
                            roles.CreateAsync(identityRole).GetAwaiter().GetResult();

                        }
                    }
                    manager.AddToRoleAsync(user, list[0].Name).GetAwaiter().GetResult();
                }

              



            }

        }
    }
}
