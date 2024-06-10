/*
 * ShlinksBotComponent.cs
 *     Created: 2024-04-25T18:50:12-04:00
 *    Modified: 2024-04-25T18:50:13-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks;

using System.Net.Http;

using Dgmjr.BotFramework;
using Dgmjr.BotFramework.Middleware;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Shlinks.Services;
using Shlinks.Security.Middleware;

public class BotComponent : CustomBotComponent
{
    public override void ConfigureServices(
        IServiceCollection services,
        IConfigurationSection configuration
    )
    {
        base.ConfigureServices(services, configuration);

        if (services.Any(sd => sd.ServiceType == typeof(IShlinksService)))
        {
            return;
        }

        services.AddBotMiddleware<ShlinksSecurityMiddleware>();

        services.AddShlinksApi(configuration);

        if (
            !services.Any(
                sd => sd.ImplementationType == typeof(TryRegisterServiceMiddleware<IShlinksService>)
            )
        )
        {
            services.AddSingleton<IBotMiddleware, TryRegisterServiceMiddleware<IShlinksService>>(
                y =>
                    new TryRegisterServiceMiddleware<IShlinksService>(
                        new ShlinksService(
                            Microsoft.Extensions.Options.Options.Create(
                                new Settings(configuration)
                            ),
                            y.GetRequiredService<ILogger<ShlinksService>>(),
                            y.GetRequiredService<IHttpClientFactory>(),
                            y.GetRequiredService<IOptions<JsonOptions>>(),
                            y.GetRequiredService<IBotTelemetryClient>()
                        )
                    )
            );
        }

        services.TryAddSingleton<IShlinksService, ShlinksService>();
        services.TryAddSingleton<IShlinksStatsService, ShlinksStatsService>();
        services.Configure<Settings>(settings =>
        {
            configuration.Bind(settings);
        });
        services.ConfigureAll<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.WriteIndented = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = JNaming.CamelCase;
            options.JsonSerializerOptions.NumberHandling = JNumbers.AllowReadingFromString;
        });
        services.AddBotMiddleware<TryRegisterServiceMiddleware<IServiceProvider>>();
        services.AddBotMiddleware<TryRegisterServiceMiddleware<IOptions<JsonOptions>>>();
        services.AddBotMiddleware<TryRegisterServiceMiddleware<IHttpClientFactory>>();
        services.AddBotMiddleware<TryRegisterServiceMiddleware<ILoggerFactory>>();
        services.AddBotMiddleware<TryRegisterServiceMiddleware<IOptions<Settings>>>();
        services.AddBotMiddleware<TryRegisterServiceMiddleware<IServiceProvider>>();
        // services.AddBotMiddleware<TryRegisterServiceMiddleware<IBotTelemetryClient>>();
        var settings = configuration.Get<Settings>();

        services.AddLoggingHttpClient<IShlinksService, ShlinksService>(
            nameof(ShlinksService),
            ApiConstants.BaseUriString,
            new StringDictionary { [HReqH.Authorization.DisplayName] = settings?.ApiKey }
        );
        services.AddLoggingHttpClient<IShlinksStatsService, ShlinksStatsService>(
            nameof(ShlinksStatsService),
            ApiConstants.StatsBaseUriString,
            new StringDictionary { [HReqH.Authorization.DisplayName] = settings?.ApiKey }
        );
    }
}
