using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jhray.com.Controllers
{
    public class MemesController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return RedirectToPage("memes/");
        }
    }
}