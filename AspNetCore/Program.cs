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
//            var config = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("hosting.json", optional: true)
//                .Build();
            CreateWebHostBuilder(args)
                .CaptureStartupErrors(true)
                .ConfigureLogging((Action<WebHostBuilderContext, ILoggingBuilder>) ((hostingContext, logging) =>
                {
                    logging.AddConfiguration((IConfiguration) hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                    logging.AddEventSourceLogger();
                    logging.AddFile(o =>
                    {
                        o.LogDirectory = AppContext.BaseDirectory;
                    });
                }))
                //.UseConfiguration(config)
                .Build()
                .Run();
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