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
using jhray.com.Models.Gems;

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
            var vm = new ChilledViewModelBuilder()
                .Configure
                .AddPodcastToGemList(_context, 0)
                .AddPicturesToGemList(_context)
                .Build<ChilledViewModel>();
            return View(vm);
        }

        public IActionResult GetRssFeed(int id)
        {
            var feed = new RSSFeed(_pathsOpt.Value).ReadFromFolderContents(_context, id);
            return Content(feed, "application/rss+xml");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact your dreams.";

            return View();
        }

        public IActionResult Podcast(int Id)
        {
            var pod = _context.Podcasts.FirstOrDefault(p => p.Id == Id);
            var retval = Content("", "application/json");
            if (pod != null)
            {
                _context.Entry(pod).Reference(p => p.GemData).Load();
                var gem = new PodcastGem
                {
                    Id = pod.Id.ToString(),
                    AudioLink = pod.Location,
                    Title = pod.GemData.Title,
                    Text = pod.Description,
                    Type = GemType.blueGem,
                    FeedId = pod.FeedId
                };
                retval = Content(Newtonsoft.Json.JsonConvert.SerializeObject(gem), "application/json");
            }
            return retval;
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
