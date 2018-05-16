using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jhray.com.Models.Gems
{
    public class PictureGem : IGem
    {
        public GemType Type { get; set; }
        public string Title { get; set; }
        public string PictureLink { get; set; }
        public string ArtistSource { get; set; }
        public string ArtistName { get; set; }
    }
}
