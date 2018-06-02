using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jhray.com.Models.GemMasterViewModels
{
    public class PodcastMetadata
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        private string Location { get; set; }
        public string ItunesDuration { get; set; }
        public DateTimeOffset PubDate { get; set; }
        private ulong LengthInBytes { get; set; }
    }
}
