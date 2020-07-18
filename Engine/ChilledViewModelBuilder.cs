using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using jhray.com.Database;
using jhray.com.Database.Entities;
using jhray.com.Models;
using jhray.com.Models.Gems;
using Microsoft.EntityFrameworkCore;
using static jhray.com.Utils.Utils;

namespace jhray.com.Engine
{
    public class ChilledViewModelBuilder
    {
        public Configuration Configure { get; set; } = new Configuration();

        public class Configuration
        {
            public List<IGem> Gems { get; set; } = new List<IGem>();
            private Random rnd = new Random();
            private Array gemTypes = Enum.GetValues(typeof(Models.Gems.GemType));

            public Configuration AddPodcastToGemList(ChilledDbContext context, int id = int.MinValue)
            {
                var podcasts = context.Podcasts.Include(p => p.GemData).Where(p => (id == int.MinValue) ? true : p.FeedId == id).OrderByDescending(p => p.PubDate).ToList();
                foreach (var pod in podcasts)
                {
                    Gems.Add(new PodcastGem
                    {
                        Id = pod.Id.ToString(),
                        AudioLink = pod.Location,
                        Title = pod.GemData.Title,
                        Text = pod.Description,
                        Type = GetRandomGemType(),
                        FeedId = pod.FeedId
                    });
                }
                return this;
            }

            private Models.Gems.GemType GetRandomGemType() => (Models.Gems.GemType)gemTypes.GetValue(rnd.Next(gemTypes.Length));

            public Configuration AddPicturesToGemList(ChilledDbContext context)
            {
                var pictures = context.Pictures.Include(p => p.GemData).OrderByDescending(p => p.CreatedDate);
                foreach (var pic in pictures)
                {
                    Gems.Add(new PictureGem
                    {
                        Id = pic.Id.ToString(),
                        Title = pic.GemData.Title,
                        Type = GetRandomGemType(),
                        PictureLink = pic.Location,
                        ArtistSource = pic.ArtistLink,
                        ArtistName = pic.ArtistName
                    });
                }
                return this;
            }

            public Configuration AddBlogsToGemList(ChilledDbContext context, int id = int.MinValue, string userId = "")
            {
                var blogs = context.BlogPosts
                        .Where(p => (id == int.MinValue) ? true : p.RSSHeader.RSSNumber == id)
                        .Include(b => b.Author)
                        .Include(b => b.Pictures)
                        .OrderByDescending(p => p.Published);
                foreach (var blog in blogs)
                {
                    if (string.IsNullOrEmpty(userId) || userId == blog.Author.Id)
                    {
                        Gems.Add(new BlogGem(blog));
                    }
                }
                return this;
            }

            public T Build<T>() where T : IContainsGemList, new()
            {
                return new T { Gems = Gems };
            }
        }
    }
}
