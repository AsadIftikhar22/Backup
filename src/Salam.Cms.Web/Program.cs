namespace Salam.Cms.Web;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

public static class Program
{
    public static void Main(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var isDevelopment = environment == Environments.Development;

        if (isDevelopment)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("App_Data/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        CreateHostBuilder(args, isDevelopment).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args, bool isDevelopment)
    {
        if (isDevelopment)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureCmsDefaults()
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(options =>
                    {
                        options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(15);
                        options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(15);
                    });
                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                    webBuilder.UseIISIntegration();
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseStaticWebAssets();
                });
        }
        else
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureCmsDefaults()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseStaticWebAssets();
                });
        }
    }
}
