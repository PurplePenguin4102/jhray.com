using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jhray.com.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jhray.com.Controllers
{
    public class GemMasterController : Controller
    {
        [AllowAnonymous]
        public IActionResult Login()
        {
            var usr = new User();
            return View(usr);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(User user)
        {

            return View(user);
        }

        public IActionResult Portal()
        {
            return View();
        }
    }
}