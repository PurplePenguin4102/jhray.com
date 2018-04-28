using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace jhray.com.Utils
{
    public static class Utils
    {
        public static Dictionary<string, string> GetLinesOfMetadata(string path)
        {
            var lines = File.ReadAllLines(path);
            return lines.ToDictionary(
                lin => lin.Substring(0, lin.IndexOf(":")),
                lin => lin.Substring(lin.IndexOf(":") + 1).Trim());
        }
    }
}
