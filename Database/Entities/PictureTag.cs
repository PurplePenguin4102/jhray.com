using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jhray.com.Database.Entities
{
    public class PictureTag
    {
        public int Id { get; set; }
        public string PictureId { get; set; }
        public string TagText { get; set; }

        public Picture PictureData { get; set; }
    }
}
