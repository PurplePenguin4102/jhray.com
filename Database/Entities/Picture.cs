using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jhray.com.Database.Entities
{
    public class Picture
    {
        [Key]
        [ForeignKey("FK_Picture_Gem")]
        public int Id { get; set; }
        public string ArtistLink { get; set; }
        public string ArtistName { get; set; }
        public string Location { get; set; }
        public long FileSize { get; set; }
        public string HoverText { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<PictureTag> PictureTags { get; set; }
        public Gem GemData { get; set; }
    }
}
