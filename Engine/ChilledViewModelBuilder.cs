using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using jhray.com.Models;
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

            public Configuration AddPodcastToGemList(string podcastFSPath)
            {
                var podcasts = Directory.EnumerateDirectories(podcastFSPath);
                foreach (var pod in podcasts)
                {

                    var files = Directory.EnumerateFiles(Path.Combine(podcastFSPath, pod))
                        .Where(f => Path.GetExtension(f) == ".mp3");
                    var metadata = GetLinesOfMetadata(Path.Combine(pod, "Metadata.txt"));
                    var uri = new UriBuilder("http", "jhray.com");
                    if (files.Count() != 1) continue;

                    var fpath = files.First();
                    var sanitized = Regex.Replace(podcastFSPath, @"\\", "/");
                    if (fpath.Contains("\\"))
                    {
                        fpath = Regex.Replace(fpath, @"\\", "/");
                    }

                    uri.Path = Regex.Replace(fpath, $"^{sanitized}", "podcast/");
                    Gems.Add(new Gem
                    {
                        AudioLink = uri.Uri.ToString(),
                        Title = metadata["title"],
                        Text = metadata["description"],
                        Type = GetRandomGemType()
                    });

                }
                return this;
            }

            private GemType GetRandomGemType()
            {
                return (GemType)gemTypes.GetValue(rnd.Next(gemTypes.Length));
            }

            public Configuration AddPicturesToGemList()
            {
                Gems.Add(new PictureGem
                {
                    Title = "D.Va Shooting Pixels",
                    Type = GetRandomGemType(),
                    PictureLink = "/images/d_va_by_liang_xing.jpg",
                    ArtistSource = "https://liang-xing.deviantart.com/art/D-VA-611312523",
                    ArtistName = "Liang-Xing"
                });
                return this;
            }

            public ChilledViewModel Build()
            {
                return new ChilledViewModel { Gems = Gems };
            }
        }
    }
}
