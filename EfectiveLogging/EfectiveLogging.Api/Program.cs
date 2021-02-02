using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Json;
using System;
using System.IO;

namespace EfectiveLogging.Api
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(path: "appSettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        public static void Main(string[] args)
        {
            ///configuração da escrita de log em arquivos
            Log.Logger = new LoggerConfiguration()
                 .ReadFrom.Configuration(Configuration)
                 //.WriteTo.File(new JsonFormatter(), @"c:\devs\log-app.json", shared: true)
                 .WriteTo.Seq("http://localhost:5341")
                 .CreateLogger();

            #region nlog

            //Log.Logger = NLogBuilder.ConfigureNlog("nlog.config").GetCurrentClassLogger();

            #endregion
            try
            {
                Log.Information(messageTemplate: "Inializando novo Host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, messageTemplate: "Problemas no host");
            }
            finally
            {
                Log.CloseAndFlush();

                //nlog
                //Nlog.LogManager.ShutDown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseSerilog();
                    //.UseNLog();
                });

    }
}
