using jhray.com.Engine;
using jhray.com.Models.Gems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jhray.com.Models.GemMasterViewModels
{
    public class PictureGemManagerViewModel : IContainsGemList
    {
        public PictureMetadata PictureMetadata { get; set; }
        public IList<IGem> Gems { get; set; }
    }
}
