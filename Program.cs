using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace jhray.com
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureKestrel(options =>
                {
                    options.Limits.MaxRequestBodySize = 150_000 * 1024; // 150,000 kB = 150MB
                    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(30);
                })
                .UseStartup<Startup>()
                .Build();
    }
}
