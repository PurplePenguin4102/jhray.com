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

            public Configuration AddPodcastToGemList(ChilledDbContext context)
            {
                foreach (var pod in context.Podcasts.OrderByDescending(p => p.PubDate))
                {
                    context.Entry(pod).Reference(p => p.GemData).Load();
                    Gems.Add(new PodcastGem
                    {
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

            public Configuration AddPicturesToGemList()
            {
                Gems.Add(new PictureGem
                {
                    Title = "D.Va Shooting Pixels",
                    Type = GetRandomGemType(),
                    PictureLink = "/uploads/pictures/d_va_by_liang_xing.jpg",
                    ArtistSource = "https://liang-xing.deviantart.com/art/D-VA-611312523",
                    ArtistName = "Liang-Xing"
                });
                return this;
            }

            public T Build<T>() where T : IContainsGemList, new()
            {
                return new T { Gems = Gems };
            }
        }
    }
}
