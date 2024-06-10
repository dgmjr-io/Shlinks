[assembly: HostingStartup(typeof(Shlinks.Bot.Configure.AppSettings))]
[assembly: HostingStartup(typeof(Shlinks.Bot.Configure.Logging))]
[assembly: HostingStartup(typeof(Shlinks.Bot.Configure.Mvc))]
[assembly: HostingStartup(typeof(Shlinks.Bot.Configure.BotComponents))]
[assembly: HostingStartup(typeof(Shlinks.Bot.Configure.Configure))]

// [assembly: HostingStartup(
//     typeof(Microsoft.Extensions.DependencyInjection.AutomaticLoggingConfigurator)
// )]
namespace Shlinks.Bot.Configure;

using Microsoft.Extensions.DependencyInjection;

public class Configure : Microsoft.AspNetCore.Builder.ConfiguratorBase<Configure>
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        // services.AddDgmjrBotRuntime(Configuration);
    }
}
