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

        [Authorize(Roles = "SuperGenius")]
        [HttpPost]
        public async Task<IActionResult> ManageUsers(ManageUsersViewModel muvm)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(muvm.NewRole))
            {
                await _roleManager.CreateAsync(new IdentityRole(muvm.NewRole));
            }
            var vm = new ManageUsersViewModel();
            vm.Roles = _roleManager.Roles.ToList();

            vm.Users = _userManager.Users.ToList();
            vm.UserRoles = new Dictionary<ChilledUser, IEnumerable<string>>();
            foreach (var usr in vm.Users)
            {
                vm.UserRoles.Add(usr, await _userManager.GetRolesAsync(usr));
            }
            return View(vm);
        }

        [Authorize(Roles = "SuperGenius")]
        [HttpGet]
        public async Task<IActionResult> AddRoleToUser(string userId, string role)
        {
            var user = _userManager.Users.First(u => u.Id == userId);
            var dbRole = _roleManager.Roles.First(r => r.Name == role);
            if (!await _userManager.IsInRoleAsync(user, dbRole.Name))
            {
                await _userManager.AddToRoleAsync(user, dbRole.Name);
            }

            return RedirectToAction("ManageUsers");
        }

        [Authorize(Roles = "SuperGenius")]
        [HttpGet]
        public async Task<IActionResult> RemoveRoleFromUser(string userId, string role)
        {
            var user = _userManager.Users.First(u => u.Id == userId);
            var dbRole = _roleManager.Roles.First(r => r.Name == role);
            if (await _userManager.IsInRoleAsync(user, dbRole.Name) && role != "SuperGenius")
            {
                await _userManager.RemoveFromRoleAsync(user, dbRole.Name);
            }

            return RedirectToAction("ManageUsers");
        }

    }
}