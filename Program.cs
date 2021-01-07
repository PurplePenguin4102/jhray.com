using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace jhray.com
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
                .ConfigureKestrel(options =>
                {
                    options.Limits.MaxRequestBodySize = 150_000 * 1024; // 150,000 kB = 150MB
                    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(30);
                    options.Listen(IPAddress.Loopback, 5002, listenOptions =>
                    {
                        listenOptions.Protocols = HttpProtocols.Http2;
                    });
                })
                .UseStartup<Startup>()
                .Build().Run();
        }    
    }
}
