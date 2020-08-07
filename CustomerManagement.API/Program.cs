using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            CreateHostBuilder().Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder()
        {

            
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                //.AddJsonFile("certificate.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"certificate.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                .Build();

            /*
            var certificateSettings = config.GetSection("certificateSettings");
            string certificateFileName = certificateSettings.GetValue<string>("filename");
            string certificatePassword = certificateSettings.GetValue<string>("password");

            var certificate = new X509Certificate2(certificateFileName, certificatePassword);
            */
            
            var host = new WebHostBuilder()
                .UseKestrel(
                    options =>
                    {
                        options.AddServerHeader = false;
                        options.Listen(IPAddress.Loopback, 5001, listenOptions =>
                        {
                           //listenOptions.UseHttps(certificate);
                        });
                    }
                )
                .UseConfiguration(config)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseSetting("detailedErrors", "false")
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("MoviesApi", LogLevel.Debug)
                    .AddConsole();
                    //logging.ClearProviders(); // removes all providers from LoggerFactory
                    //logging.AddConsole();
                }
                )
                .UseUrls("http://*:5001;http://localhost:5001;https://localhost:5001");


            return host;
        }
    }
}