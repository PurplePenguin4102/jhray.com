using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jhray.com.Models;
using jhray.com.Engine;
using Microsoft.Extensions.Options;

namespace jhray.com.Controllers
{
    public class HomeController : Controller
    {
        private IOptions<Paths> _pathsOpt;

        public HomeController(IOptions<Paths> pathsOpt)
        {
            _pathsOpt = pathsOpt;
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
                .AddPodcastToGemList(direc)
                .AddPicturesToGemList()
                .Build();
            return View(vm);
        }


        public IActionResult GetRssFeed(DateTime date)
        {
            var feed = new RSSFeed().ReadFromFolderContents(_pathsOpt.Value.PodcastDirectory);
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
