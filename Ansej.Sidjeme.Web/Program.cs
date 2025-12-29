
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ansej.Sidjeme.Web
{
    public class Program
    {
        private static readonly string _tempFolderPath = @"C:\TMP";
        public static void Main(string[] args)
        {
            // Créer le répertoire TMP s'il n'existe pas
            if (!Directory.Exists(_tempFolderPath))
                Directory.CreateDirectory(_tempFolderPath);

            Environment.SetEnvironmentVariable("TMP", _tempFolderPath);
            Environment.SetEnvironmentVariable("TEMP", _tempFolderPath);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
