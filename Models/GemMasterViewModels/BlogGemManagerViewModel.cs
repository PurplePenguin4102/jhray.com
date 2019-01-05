using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jhray.com.Engine;
using jhray.com.Models.Gems;

namespace jhray.com.Models.GemMasterViewModels
{
    public class BlogGemManagerViewModel : IContainsGemList
    {
        public IList<IGem> Gems { get; set; }
    }
}
