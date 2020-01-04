using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RadoHub.Data.Models;
using RadoHub.Services.Constants;
using System.Linq;
using System.Threading.Tasks;

namespace RadoHub.WebApp.Middlewares
{
    public class SeedDataMiddleware
    {
        private readonly RequestDelegate _next;

        public SeedDataMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<RadoHubUser> userManager,
                                      RoleManager<IdentityRole> roleManager)
        {
            SeedRole(roleManager, GlobalConstants.AdminRole).GetAwaiter().GetResult();
            SeedRole(roleManager, GlobalConstants.DefaultUserRole).GetAwaiter().GetResult();

            SeedUserInRoles(userManager).GetAwaiter().GetResult();

            await _next(context);
        }

        private static async Task SeedRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        private static async Task SeedUserInRoles(UserManager<RadoHubUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new RadoHubUser
                {
                    UserName = "admin",
                    Email = "admin@radohub.com",
                    FirstName = "AdminFirstName",
                    LastName = "AdminLastName",
                };

                var password = "123456";

                var userCreated = await userManager.CreateAsync(user, password);

                if (userCreated.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, GlobalConstants.AdminRole);
                    await userManager.AddToRoleAsync(user, GlobalConstants.DefaultUserRole);
                }
            }
        }
    }
}
