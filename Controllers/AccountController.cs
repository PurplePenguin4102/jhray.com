using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jhray.com.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public IActionResult Unauthorized()
        {
            return View();
        }

        public IActionResult Forbidden()
        {
            return View();
        }

        public IActionResult WhoAmI()
        {
            return View();
        }
    }
}