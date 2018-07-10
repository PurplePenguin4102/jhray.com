using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jhray.com.Database.Entities
{
    public class Picture
    {
        public int Id { get; set; }
        public string ArtistLink { get; set; }
        public string HoverText { get; set; }

        public List<PictureTag> PictureTags { get; set; }
        public Gem GemData { get; set; }
    }
}
