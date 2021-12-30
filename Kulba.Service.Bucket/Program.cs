using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kulba.Service.Bucket.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Debugging;

namespace Kulba.Service.Bucket
{
    public static class Program
    {
        private static string _baseProjectPath;

        public static void Main(string[] args)
        {
            SelfLog.Enable(Console.Error);

            try
            {
                _baseProjectPath = AppDomain.CurrentDomain.BaseDirectory;

                var configuration = new ConfigurationBuilder()
                    .AddJsonFile(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.ToLower() == "development" ? "appsettings-test.json" : "appsettings.json")
                    .Build();

                Log.Logger = new LoggerConfiguration()                                    
                    .ReadFrom.Configuration(configuration)
                    .CreateBootstrapLogger();
            
                Log.Warning("Starting Conversion Service...");
                Log.Information("====================================================================");
                Log.Information($"Application Version: {System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version}");
                Log.Information($"Application Directory: {_baseProjectPath}");

                CreateHostBuilder(args).Build().Run();
            }
            catch (OperationCanceledException)
            {
                Log.Information("Cancellation requested host has stopped");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .WriteTo.BucketSink().WriteTo.Console())
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }

}
