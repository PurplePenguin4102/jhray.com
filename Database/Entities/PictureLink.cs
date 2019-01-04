using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jhray.com.Database.Entities
{
    public class PictureLink
    {
        public int Id { get; set; }

        [ForeignKey("PictureLink_Blog_FK")]
        public int BlogId { get; set; }
        [ForeignKey("PictureLink_Picture_FK")]
        public int PictureId { get; set; }

        public BlogPost Blog { get; set; }
        public Picture Picture { get; set; }
    }
}
