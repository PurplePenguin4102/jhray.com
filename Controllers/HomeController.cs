using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jhray.com.Models;
using jhray.com.Engine;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using jhray.com.Database;
using jhray.com.Models.Gems;
using System.Net.Http.Headers;

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
                .AddBlogsToGemList(_context, 0)
                .Build<ChilledViewModel>();
            return View(vm);
        }

        public IActionResult STSPodcastAU()
        {
            var vm = new ChilledViewModelBuilder()
                .Configure
                .AddPodcastToGemList(_context, 2)
                .AddBlogsToGemList(_context, 2)
                .Build<ChilledViewModel>();
            return View(vm);
        }

        public IActionResult YowiePowerHour()
        {
            using (var client = new HttpClient())
            {
                //var token = await GetBase64EncodedCredential();

                //client.BaseAddress = new Uri("https://api.twitter.com");
                //client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("JHRaySync", "1.0")));
                //client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
                //var httpRequest = new HttpRequestMessage(HttpMethod.Post, "/oauth2/token");
                //httpRequest.Content = new FormUrlEncodedContent(new Dictionary<string, string> { { "grant_type", "client_credentials" } });
                //var authResponse = await client.SendAsync(httpRequest);
                
                //var resp = await client.GetAsync("https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name=twitterapi&count=2");
            }
            var vm = new ChilledViewModelBuilder()
                .Configure
                .AddPodcastToGemList(_context, 1)
                .Build<ChilledViewModel>();
            return View(vm);
        }

        public async Task<string> GetBase64EncodedCredential()
        {
            var keyFile = await System.IO.File.ReadAllLinesAsync(_pathsOpt.Value.CredsDirectory + "AccessKey.txt");
            var accessToken = keyFile[0];
            var accessTokenSecret = keyFile[1];
            var consumerKey = Uri.EscapeUriString(keyFile[2]);
            var consumerSecret = Uri.EscapeUriString(keyFile[3]);
            var fullConsumerToken = consumerKey + ":" + consumerSecret;
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(fullConsumerToken));
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
            var pod = _context.Podcasts.Include(p => p.GemData).FirstOrDefault(p => p.Id == Id);
            var retval = Content("", "application/json");
            if (pod != null)
            {
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