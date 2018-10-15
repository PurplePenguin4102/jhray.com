using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jhray.com.Engine;
using jhray.com.Models.Gems;

namespace jhray.com.Models.GemMasterViewModels
{
    public class GemManagerViewModel : IContainsGemList
    {
        public PodcastMetadata PodcastMetadata { get; set; }
        public PictureMetadata PictureMetadata { get; set; }
        public IList<IGem> Gems { get; set; }
    }
}
