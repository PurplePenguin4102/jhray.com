﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jhray.com.Models.Gems
{
    public interface IGem
    {
        GemType Type { get; }
        string Title { get; }
        string Id { get; }
    }
}
