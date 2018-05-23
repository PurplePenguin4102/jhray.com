using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jhray.com.Database.Entities;
using jhray.com.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace jhray.com.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<ChilledUser> _userManager;
        private readonly SignInManager<ChilledUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger _logger;

        public AccountController(UserManager<ChilledUser> userManager,
           SignInManager<ChilledUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           ILogger<GemMasterController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        [AllowAnonymous]
        public IActionResult Unauthorized()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Forbidden()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult WhoAmI()
        {
            return View();
        }

        [Authorize(Roles = "SuperGenius")]
        public async Task<IActionResult> ManageUsers()
        {
            var vm = new ManageUsersViewModel();
            vm.Roles = _roleManager.Roles.ToList();

            vm.Users = _userManager.Users.ToList();
            vm.UserRoles = new Dictionary<ChilledUser, IEnumerable<string>>();
            foreach (var usr in vm.Users)
            {
                vm.UserRoles.Add(usr,await _userManager.GetRolesAsync(usr));
            }
            return View(vm);
        }
    }
}