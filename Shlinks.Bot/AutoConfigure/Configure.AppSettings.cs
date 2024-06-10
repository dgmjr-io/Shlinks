namespace Shlinks.Bot.Configure;

using Dgmjr.BotFramework;

using global::Azure.Identity;

using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Settings.Configuration;

using static System.IO.Path;

using ConfigurationBuilderExtensions = Microsoft.Bot.Builder.Dialogs.Adaptive.Runtime.Extensions.ConfigurationBuilderExtensions;

public class AppSettings : ConfiguratorBase<AppSettings>
{
    private const string AzureAppConfig = nameof(AzureAppConfig);

    // protected override void ConfigureAppConfiguration(
    //     WebHostBuilderContext context,
    //     IConfigurationBuilder config
    // )
    // {
    //     Logger.ConfiguringService(
    //         $"{nameof(Configure)}.{nameof(AppSettings)}",
    //         context.HostingEnvironment.EnvironmentName
    //     );

    //     var applicationRoot = context.HostingEnvironment.ContentRootPath;
    //     var environmentName = context.HostingEnvironment.EnvironmentName;
    //     var settingsDirectory = "settings";

    //     config.AddBotRuntimeConfiguration(applicationRoot, settingsDirectory, environmentName);

    //     Console.WriteLine("Bot runtime loaded");

    //     config.AddUserSecrets<AppSettings>();
    //     Console.WriteLine("User secrets loaded");

    //     config.AddKeyPerJsonFile(
    //         Join(GetDirectoryName(typeof(AppSettings).Assembly.Location), settingsDirectory),
    //         settingsDirectory,
    //         true,
    //         false
    //     );
    //     Console.WriteLine("Key per JSON files loaded");

    //     // try
    //     // {
    //     //     var azureAppConfigConnectionString = new ConfigurationBuilder()
    //     //         .AddUserSecrets<AppSettings>()
    //     //         .Build()
    //     //         .GetConnectionString(AzureAppConfig);
    //     //     config.AddAzureAppConfiguration(
    //     //         options =>
    //     //             options
    //     //                 .Connect(azureAppConfigConnectionString)
    //     //                 .ConfigureKeyVault(
    //     //                     options => options.SetCredential(new DefaultAzureCredential())
    //     //                 )
    //     //     );
    //     //     Console.WriteLine("Azure app config loaded");
    //     // }
    //     // catch (Exception ex)
    //     // {
    //     //     Console.WriteLine(
    //     //         $"An exception occurred whilst loading the Azure app config: {ex.Message}\n\n{ex.StackTrace}"
    //     //     );
    //     // }

    //     // config.AddKeyPerJsonFile(
    //     //     "/" + settingsDirectory,
    //     //     true,
    //     //     true,
    //     //     new SerilogLoggerFactory(
    //     //         new Serilog.LoggerConfiguration().CreateBootstrapLogger()
    //     //     ).CreateLogger<AppSettings>()
    //     // );

    //     // Log.Logger = new LoggerConfiguration()
    //     //     .ReadFrom.Configuration(
    //     //         (config as IConfigurationManager)?.GetSection(nameof(Serilog))
    //     //             ?? (ConfigurationSection)config
    //     //     )
    //     //     .CreateLogger();
    //     // Console.WriteLine("Config: {0}", config.ToJson());
    //     Serilog.Log.Logger = new LoggerConfiguration().ReadFrom
    //         .Configuration(config as IConfigurationManager, nameof(Serilog))
    //         .CreateLogger();

    //     // Console.WriteLine((config as IConfigurationRoot)!.GetDebugView());
    // }
}
