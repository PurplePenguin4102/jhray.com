using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jhray.com.Models;
using jhray.com.Engine;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using jhray.com.Database;

namespace jhray.com.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IOptions<Paths> _pathsOpt;
        private readonly ChilledDbContext _context;

        public HomeController(IOptions<Paths> pathsOpt, ChilledDbContext context)
        {
            _pathsOpt = pathsOpt;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChilledESports()
        {
            var direc = _pathsOpt.Value.PodcastDirectory;
            var vm = new ChilledViewModelBuilder()
                .Configure
                .AddPodcastToGemList(direc, _context)
                .AddPicturesToGemList()
                .Build<ChilledViewModel>();
            return View(vm);
        }

        public IActionResult GetRssFeed(DateTime date)
        {
            var feed = new RSSFeed(_pathsOpt.Value).ReadFromFolderContents(_context);
            return Content(feed, "application/rss+xml");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact your dreams.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
