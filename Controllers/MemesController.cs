using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace jhray.com.Controllers
{
    public class MemesController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToPage("memes/");
        }
    }
}