using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
                    var uri = new UriBuilder("http", "jhray.com");
                    if (files.Count() != 1) continue;

                    var fpath = files.First();
                    var sanitized = Regex.Replace(value, @"\\", "/");
                    if (fpath.Contains("\\"))
                    {
                        fpath = Regex.Replace(fpath, @"\\", "/");
                    }

                    uri.Path = Regex.Replace(fpath, $"^{sanitized}", "podcast/");
                    AudioLinks.Add(uri.Uri.ToString());
                }
            }
        }

        public List<string> AudioLinks { get; set; } = new List<string>();
        
    }
}
