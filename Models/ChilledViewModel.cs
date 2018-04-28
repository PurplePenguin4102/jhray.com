using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static jhray.com.Utils.Utils;

namespace jhray.com.Models
{
    public class ChilledViewModel
    {
        public string PodcastFSPath
        { 
            set 
            {
                var podcasts = Directory.EnumerateDirectories(value);
                foreach (var pod in podcasts)
                {

                    var files = Directory.EnumerateFiles(Path.Combine(value, pod)).Where(f => Path.GetExtension(f) == ".mp3");
                    var metadata = GetLinesOfMetadata(Path.Combine(pod, "Metadata.txt"));
                    var uri = new UriBuilder("http", "jhray.com");
                    if (files.Count() != 1) continue;

                    var fpath = files.First();
                    var sanitized = Regex.Replace(value, @"\\", "/");
                    if (fpath.Contains("\\"))
                    {
                        fpath = Regex.Replace(fpath, @"\\", "/");
                    }

                    uri.Path = Regex.Replace(fpath, $"^{sanitized}", "podcast/");
                    Gems.Add(new Gem
                    {
                        AudioLink = uri.Uri.ToString(),
                        Title = metadata["title"],
                        Text = metadata["description"]
                    });

                }
            }
        }

        public List<Gem> Gems { get; set; } = new List<Gem>();
      
    }
}
