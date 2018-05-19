using jhray.com.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jhray.com.Database
{
    public class SeedDatabase
    {
        public static async void Go(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService(typeof(ChilledDbContext)) as ChilledDbContext;

            await AssignRoles(serviceProvider, "joseph.h.ray@gmail.com");

            await context.SaveChangesAsync();
        }

        public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email)
        {
            var _userManager = services.GetService(typeof(UserManager<ChilledUser>)) as UserManager<ChilledUser>;
            var _roleManager = services.GetService(typeof(RoleManager<IdentityRole>)) as RoleManager<IdentityRole>;

            if (!await _roleManager.RoleExistsAsync("SuperGenius"))
            {
                await _roleManager.CreateAsync(new IdentityRole("SuperGenius"));
            }

            var user = await _userManager.FindByEmailAsync(email);
            var roles = _roleManager.Roles;
            if (user != null)
            {
                var result = await _userManager.AddToRoleAsync(user, roles.Last().Name);
                return result;
            }
            return IdentityResult.Failed();
        }
    }
}
