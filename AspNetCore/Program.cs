using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AspNetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
    public class SsoSettings
    {
        public const string DefaultPassword = "1qaz2wsx";
        public const string HashSuffix = "LanteriaHR";
        public string HashPrefix { get; set; }
        public string SuccessUrl { get; set; }
        public string ErrorUrl { get; set; }
    }
}