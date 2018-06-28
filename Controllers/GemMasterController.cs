using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jhray.com.Models.GemMasterViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using jhray.com.Database.Entities;
using jhray.com.Services;
using jhray.com.Database;
using System.Security.Claims;
using System.Threading;
using jhray.com.Engine;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace jhray.com.Controllers
{
    public class GemMasterController : Controller
    {
        private readonly UserManager<ChilledUser> _userManager;
        private readonly SignInManager<ChilledUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger _logger;
        private readonly IEmailSender _emailSender;
        private readonly ChilledDbContext _context;
        private readonly IOptions<Paths> _pathsOpt;

        public GemMasterController(UserManager<ChilledUser> userManager,
                                   SignInManager<ChilledUser> signInManager,
                                   RoleManager<IdentityRole> roleManager,
                                   ILogger<GemMasterController> logger,
                                   ChilledDbContext context,
                                   IEmailSender emailSender,
                                   IOptions<Paths> pathsOpt)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
            _pathsOpt = pathsOpt;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid && model.Password == model.ConfirmPassword)
            {
                var user = new ChilledUser { UserName = model.Name, Email = model.Email, Joined = DateTimeOffset.Now };
                
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("ConfirmEmail", "GemMaster", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                        "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User created a new account with password.");
                    await _userManager.ConfirmEmailAsync(user, code);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        public IActionResult Myself()
        {
            return View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        [Authorize(Roles = "RegularGenius")]
        public IActionResult GemManager()
        {
            var direc = _pathsOpt.Value.PodcastDirectory;
            var vm = new ChilledViewModelBuilder()
                .Configure
                .AddPodcastToGemList(direc, _context)
                .Build<GemManagerViewModel>();
            return View(vm);
        }

        [Authorize(Roles = "RegularGenius")]
        [HttpGet]
        public IActionResult AddGem()
        {
            return RedirectToAction("GemManager");
        }

        [Authorize(Roles = "RegularGenius")]
        [RequestSizeLimit(150_000_000)]
        [HttpPost]
        public async Task<IActionResult> AddGem(GemManagerViewModel gem)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                await new RSSFeed(_pathsOpt.Value).CreateNewEpisode(gem.PodcastMetadata, _context, userId);
            }
            return RedirectToAction("GemManager");
        }

        [Authorize(Roles = "SuperGenius")]
        [HttpGet]
        public async Task<IActionResult> SeedDB()
        {
            await SeedDatabase.Go(_context, _pathsOpt.Value, _userManager, _roleManager, "joseph.h.ray@gmail.com");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [Authorize(Roles="RegularGenius")]
        [HttpGet]
        public IActionResult DeletePodcast(int id)
        {
            new RSSFeed(_pathsOpt.Value).DeleteParticularEp(id);
            return RedirectToAction("GemManager");
        }
    }
}