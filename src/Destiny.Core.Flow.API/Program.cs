using AspectCore.Extensions.Hosting;
using Destiny.Core.Flow.SeriLog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace Destiny.Core.Flow.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //SeriLogLogger.SetSeriLoggerToFile("Logs",string.Empty);
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.ConfigureServices((web, s) =>
                    //{
                    //    web.HostingEnvironment.IsDevelopment();


                    //}).UseStartup<Startup>()
                    webBuilder.UseStartup<Startup>()

                 
                 .UseSerilog((webHostBuilderContext, loggerConfiguration) => {
                     var configuration = loggerConfiguration;
                     var isDev= webHostBuilderContext.HostingEnvironment.IsDevelopment();
                     if (isDev)
                     {
                         configuration = configuration.MinimumLevel.Information()
                         .MinimumLevel.Override("Microsoft", LogEventLevel.Information);

                     }
                     else {
                         configuration = loggerConfiguration.MinimumLevel.Error().MinimumLevel
                        .Override("Microsoft", LogEventLevel.Error);
                     }

                     configuration.Enrich.FromLogContext().WriteTo.Console(isDev? LogEventLevel.Information: LogEventLevel.Error)
                        .WriteTo.Map(le => MapData(le),
             (key, log) =>
              log.Async(o => o.File(Path.Combine("Logs", @$"{key.time:yyyy-MM-dd}\{key.level.ToString().ToLower()}.txt"))));

                     (DateTime time, LogEventLevel level) MapData(LogEvent logEvent)
                     {

                         return (new DateTime(logEvent.Timestamp.Year, logEvent.Timestamp.Month, logEvent.Timestamp.Day, logEvent.Timestamp.Hour, logEvent.Timestamp.Minute, logEvent.Timestamp.Second), logEvent.Level);
                     }

                 }).ConfigureLogging((hostingContext, builder) =>
                   {
                       builder.ClearProviders();
                       builder.SetMinimumLevel(LogLevel.Information);

                       builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                       builder.AddConsole();
                       builder.AddDebug();
                   });
                }).UseDynamicProxy();//*/;//aspcectcore;


    }
}
