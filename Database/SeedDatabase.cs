using jhray.com.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace jhray.com.Database
{
    public class SeedDatabase
    {
        public static async Task<IdentityResult> Go(ChilledDbContext context, Paths paths, UserManager<ChilledUser> userManager, RoleManager<IdentityRole> roleManager, string email)
        {
            if (!await roleManager.RoleExistsAsync("SuperGenius"))
            {
                await roleManager.CreateAsync(new IdentityRole("SuperGenius"));
            }

            var user = await userManager.FindByEmailAsync(email);
            var role = await roleManager.FindByNameAsync("SuperGenius");
            if (user != null && !await userManager.IsInRoleAsync(user, role.Name))
            {
                var result = await userManager.AddToRoleAsync(user, role.Name);
                return result;
            }
            await context.SaveChangesAsync();
            
            return IdentityResult.Failed();
        }
    }
}
