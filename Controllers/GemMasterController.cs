using System;
using System.Collections.Generic;
using System.IO;
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
using jhray.com.Models.Gems;
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
        private readonly ChilledDbContext _context;
        private readonly IOptions<Paths> _pathsOpt;

        public GemMasterController(UserManager<ChilledUser> userManager, ChilledDbContext context, IOptions<Paths> pathsOpt)
        {
            _userManager = userManager;
            _context = context;
            _pathsOpt = pathsOpt;
        }

        [Authorize(Roles = "RegularGenius")]
        public IActionResult PodcastGemManager()
        {
            return View(new ChilledViewModelBuilder().Configure.AddPodcastToGemList(_context).Build<PodcastGemManagerViewModel>());
        }

        [Authorize(Roles = "RegularGenius")]
        public IActionResult PictureGemManager()
        {
            return View(new ChilledViewModelBuilder().Configure.AddPicturesToGemList(_context).Build<PictureGemManagerViewModel>());
        }

        [Authorize(Roles = "RegularGenius")]
        [HttpGet]
        public IActionResult BlogGemManager()
        {
            return View(new ChilledViewModelBuilder().Configure.AddBlogsToGemList(_context, userId: _userManager.GetUserId(User)).Build<BlogGemManagerViewModel>());
        }

        [Authorize(Roles = "RegularGenius")]
        [HttpGet]
        public IActionResult AddGem()
        {
            return RedirectToAction("PodcastGemManager");
        }

        [Authorize(Roles = "RegularGenius")]
        [RequestSizeLimit(150_000_000)]
        [HttpPost]
        public async Task<IActionResult> AddGem(PodcastGemManagerViewModel gem)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                if (gem.PodcastMetadata != null)
                {
                    await new RSSFeed(_pathsOpt.Value).CreateNewEpisode(gem.PodcastMetadata, _context, userId);
                }
            }
            return RedirectToAction("PodcastGemManager");
        }

        [Authorize(Roles = "RegularGenius")]
        [RequestSizeLimit(5_000_000)]
        [HttpPost]
        public async Task<IActionResult> AddPictureGem(PictureGemManagerViewModel gem)
        {
            
            if (ModelState.IsValid && gem.PictureMetadata != null && gem.PictureMetadata.PictureFile.ContentType.StartsWith("image"))
            {
                var svc = new PictureService();
                await svc.SavePictureToDatabase(gem, await _userManager.GetUserAsync(User), _pathsOpt.Value, _context);
            }
            return RedirectToAction("PictureGemManager");
        }

        [Authorize(Roles = "SuperGenius")]
        [HttpGet]
        public IActionResult SeedDB()
        {
            //await SeedDatabase.Go(_context, _pathsOpt.Value, _userManager, _roleManager, "joseph.h.ray@gmail.com");
            //SeedDatabase.FillOutPodcastMeta(_context, _pathsOpt.Value);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [Authorize(Roles = "RegularGenius")]
        [HttpGet]
        public IActionResult NewBlogPost()
        {
            return View("BlogPostEditor", new BlogGem()
            {
                AuthorName = _userManager.GetUserName(User),
                Published = DateTime.Now
            });
        }

        [Authorize(Roles = "RegularGenius")]
        [HttpPost]
        public async Task<IActionResult> AddEditBlogPost(BlogGem blog)
        {
            var svc = new BlogService();
            if (string.IsNullOrEmpty(blog.Id))
            {
                svc.AddNewBlogPost(blog, _context, await _userManager.GetUserAsync(User));
            }
            else
            {
                svc.EditBlogPost(blog, _context);
            }
            return RedirectToAction("BlogGemManager");
        }

        [Authorize(Roles = "RegularGenius")]
        [HttpPost]
        public IActionResult DeleteBlogPost(int id)
        {
            return RedirectToAction("BlogGemManager");
        }

        [Authorize(Roles = "RegularGenius")]
        [HttpGet]
        public async Task<IActionResult> EditBlogPost(int id)
        {
            var blog = _context.BlogPosts.FirstOrDefault(b => b.Id == id);
            _context.Entry(blog).Collection(p => p.Pictures).Load();
            _context.Entry(blog).Reference(p => p.Author).Load();
            var usr = await _userManager.GetUserAsync(User);
            if (usr.Id == blog.Author.Id)
            {
                return View("BlogPostEditor", new BlogGem(blog));
            }
            return RedirectToAction("BlogGemManager");
        }

        [Authorize(Roles="RegularGenius")]
        [HttpGet]
        public IActionResult DeletePodcast(int id)
        {
            var podcast = _context.Podcasts.FirstOrDefault(p => p.Id == id);
            _context.Entry(podcast).Reference(pod => pod.GemData).Load();
            if (podcast != null)
            {
                _context.Podcasts.Remove(podcast);
                _context.Gems.Remove(podcast.GemData);
                _context.SaveChanges();
                System.IO.File.Delete(podcast.GemData.FilePath);
            }
            return RedirectToAction("PodcastGemManager");
        }

        [Authorize(Roles = "RegularGenius")]
        [HttpGet]
        public IActionResult DeletePicture(int id)
        {
            var picture = _context.Pictures.FirstOrDefault(p => p.Id == id);
            _context.Entry(picture).Reference(pic => pic.GemData).Load(); 
            if (picture != null)
            {
                _context.Pictures.Remove(picture);
                _context.Gems.Remove(picture.GemData);
                _context.SaveChanges();
                System.IO.File.Delete(picture.GemData.FilePath);
            }
            return RedirectToAction("PictureGemManager");
        }

        [Authorize(Roles = "SuperGenius")]
        [HttpGet]
        public IActionResult AddPodcast()
        {
            return View(new AddPodcastViewModel
            {
                RSSHeaders = _context.RSSHeaders.ToList()
            });
        }

        [Authorize(Roles = "SuperGenius")]
        [HttpPost]
        public IActionResult AddPodcast(AddPodcastViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var newHeader = vm.NewHeader;
                _context.RSSHeaders.Add(newHeader);
                _context.SaveChanges();
            }
            return View(new AddPodcastViewModel
            {
                RSSHeaders = _context.RSSHeaders.ToList()
            });
        }
    }
}