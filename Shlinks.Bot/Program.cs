// using System;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.Bot.Builder.Dialogs.Adaptive.Runtime.Extensions;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Hosting;
// using Serilog;

// namespace Shlinks.Bot
// {
//     public class Program
//     {
//         public static void Main(string[] args)
//         {
//             CreateHostBuilder(args).Build().Run();
//         }

//         public static IHostBuilder CreateHostBuilder(string[] args) =>
//             Host.CreateDefaultBuilder(args)
//                 .ConfigureAppConfiguration(
//                     (hostingContext, builder) =>
//                     {
//                         var applicationRoot = AppDomain.CurrentDomain.BaseDirectory;
//                         var environmentName = hostingContext.HostingEnvironment.EnvironmentName;
//                         var settingsDirectory = "settings";

//                         Microsoft.Bot.Builder.Dialogs.Adaptive.Runtime.Extensions.ConfigurationBuilderExtensions.AddBotRuntimeConfiguration(
//                             builder,
//                             applicationRoot,
//                             settingsDirectory,
//                             environmentName
//                         );

//                         builder.AddCommandLine(args);
//                     }
//                 )
//                 .ConfigureWebHostDefaults(webBuilder =>
//                 {
//                     webBuilder.UseStartup<Startup>();
//                 })
//                 .UseSerilog(
//                     (hostingContext, loggerConfiguration) =>
//                     {
//                         loggerConfiguration.ReadFrom
//                             .Configuration(hostingContext.Configuration)
//                             .Enrich.FromLogContext() /*
//                             .WriteTo.Console()*/
//                         ;
//                     }
//                 );
//     }
// }

using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;

using Serilog;

using static System.IO.Path;

using Log = Serilog.Log;
using WebApplication = Microsoft.AspNetCore.Builder.WebApplication;

const string Mvc = nameof(Mvc);

try
{
    Log.Logger = new LoggerConfiguration().MinimumLevel
        .Debug()
        .WriteTo.Console()
        .CreateBootstrapLogger();

    var builder = WebApplication.CreateBuilder(args);

    Console.WriteLine("Bot runtime loaded");

    builder.Configuration.AddUserSecrets<AppSettings>();
    Console.WriteLine("User secrets loaded");

    var applicationRoot = builder.Environment.ContentRootPath;
    var environmentName = builder.Environment.EnvironmentName;
    var settingsDirectory = "settings";

    Console.WriteLine($"applicationRoot: {applicationRoot}");
    Console.WriteLine($"environmentName: {environmentName}");

    builder.Configuration.AddBotRuntimeConfiguration(
        applicationRoot,
        settingsDirectory,
        environmentName
    );

    builder.Configuration.AddKeyPerJsonFile(
        Join(GetDirectoryName(typeof(AppSettings).Assembly.Location), settingsDirectory),
        settingsDirectory,
        true,
        false
    );

    builder.Host.UseSerilog(
        (hostingContext, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom
                .Configuration(hostingContext.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console();
        }
    );

    StaticLogger
        .GetLogger<Program>()
        .JsonFileContent((builder.Configuration as IConfiguration).ToJson());

    builder.Services.AddDgmjrBotRuntime(builder.Configuration);
    builder.Services.AddAuthorization();
    builder.Services.AddControllers();

    Console.WriteLine("Key per JSON files loaded");

    Log.Logger = new LoggerConfiguration().ReadFrom
        .Configuration(builder.Configuration, nameof(Serilog))
        .CreateLogger();

    var app = builder.Build();
    var config = app.Configuration;
    app.Logger.LogInformation(config.ToJson());
    app.Urls.Add("http://localhost:8080");
    app.Urls.Add("http://localhost:3978");
    app.UseDefaultFiles();

    // Set up custom content types - associating file extension to MIME type.
    var provider = new FileExtensionContentTypeProvider();
    provider.Mappings[".lu"] = "application/vnd.microsoft.lu";
    provider.Mappings[".qna"] = "application/vnd.microsoft.qna";

    // Expose static files in manifests folder for skill scenarios.
    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.UseStaticFiles(new StaticFileOptions { ContentTypeProvider = provider });
    app.UseWebSockets();
    app.UseRouting();
    app.UseAuthorization();
    app.UseStatusCodePages();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
