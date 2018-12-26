using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using jhray.com.Database;
using jhray.com.Models;
using jhray.com.Models.Gems;
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
            private Array gemTypes = Enum.GetValues(typeof(GemType));

            public Configuration AddPodcastToGemList(ChilledDbContext context, int id = int.MinValue)
            {
                foreach (var pod in context.Podcasts.Where(p => (id == int.MinValue) ? true : p.FeedId == id).OrderByDescending(p => p.PubDate))
                {
                    context.Entry(pod).Reference(p => p.GemData).Load();
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

            private GemType GetRandomGemType() => (GemType)gemTypes.GetValue(rnd.Next(gemTypes.Length));

            public Configuration AddPicturesToGemList(ChilledDbContext context)
            {
                foreach (var pic in context.Pictures.OrderByDescending(p => p.CreatedDate))
                {
                    context.Entry(pic).Reference(p => p.GemData).Load();
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

            public T Build<T>() where T : IContainsGemList, new()
            {
                return new T { Gems = Gems };
            }
        }
    }
}
