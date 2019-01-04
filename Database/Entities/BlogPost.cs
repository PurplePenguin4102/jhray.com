using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jhray.com.Database.Entities
{
    public class BlogPost
    {
        [Key]
        public int id { get; set; }
        public string Hashtags { get; set; }
        public string MarkdownContent { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public DateTime Published { get; set; }

        [ForeignKey("RSSHeader_BlogPost_FK")]
        public int RSSHeaderId { get; set; }
        public RSSHeader RSSHeader { get; set; }

        [ForeignKey("ChilledUser_BlogPost_FK")]
        public int AuthorId { get; set; }
        
        public ChilledUser Author { get; set; }
        public List<PictureLink> Pictures { get; set; }
    }
}
