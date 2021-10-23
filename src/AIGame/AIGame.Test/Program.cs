using System;
using System.Threading.Tasks;
using AIGame.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AIGame.Test
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureGame()
                .Build();
            
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {SourceContext}] {Message}{NewLine}{Exception}")
                .WriteTo.File(
                    "Logs/log_.txt", 
                    rollingInterval: RollingInterval.Day, 
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {SourceContext}] {Message}{NewLine}{Exception}")
                .CreateLogger();

            try
            {
                Log.Information("Running host");
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Crashed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
