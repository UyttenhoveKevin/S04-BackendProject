using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spinning.Models.Data
{
    public class SeedDatabase
    {

        public static async Task Initialize(SpinningDBContext context, UserManager<SpinningUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            context.Database.EnsureCreated();

            const string adminRole = "Admin";
            const string userRole = "User";


            const string userPassword = "Docent@1";
            const string userName = "Docent@MCT";

            if (await roleManager.FindByNameAsync(adminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            if (await roleManager.FindByNameAsync(userRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(userRole));
            }


            if (await userManager.FindByNameAsync(userName) == null)
            {
                var user = new SpinningUser
                {
                    UserName = userName,
                    Email = userName
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, userPassword);
                    await userManager.AddToRoleAsync(user, adminRole);
                }
            }

        }
    }
}
