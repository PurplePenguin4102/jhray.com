using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jhray.com.Database.Entities
{
    public class PictureTag
    {
        public int Id { get; set; }
        [ForeignKey("FK_PictureTag_Picture")]
        public int PictureId { get; set; }
        public string TagText { get; set; }

        public Picture PictureData { get; set; }
    }
}
