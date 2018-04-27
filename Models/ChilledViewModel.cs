using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace jhray.com.Models
{
    public class ChilledViewModel
    {
        public string PodcastFSPath { get; set; }
        public List<string> AudioLinks { get; set; }
        private string _baseAddress = "http://jhray.com/podcast";

        public ChilledViewModel()
        {
            var podcasts = Directory.EnumerateDirectories(PodcastFSPath);
            foreach (var pod in podcasts)
            {

                var files = Directory.EnumerateFiles(Path.Combine(PodcastFSPath, pod)).Where(f => Path.GetExtension(f) == ".mp3");

            }
        }
    }
}
