using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jhray.com.Models;
using Microsoft.AspNetCore.Mvc;

namespace jhray.com.Controllers
{
    public class GemMasterController : Controller
    {
        public IActionResult Login()
        {
            var usr = new User();
            return View(usr);
        }

        [HttpPost]
        public IActionResult Login(User user)
        {

            return View(user);
        }
    }
}