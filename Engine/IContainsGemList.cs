using jhray.com.Models.Gems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jhray.com.Engine
{
    public interface IContainsGemList
    {
        IList<IGem> Gems { get; set; }
    }
}
