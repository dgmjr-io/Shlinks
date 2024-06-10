/*
 * ShlinksApiServiceCollectionExtensions.cs
 *     Created: 2024-04-15T23:04:58-04:00
 *    Modified: 2024-04-23T15:25:40-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Microsoft.Extensions.DependencyInjection;

using System.Net.Http;

using Dgmjr.BotFramework.Middleware;

using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Shlinks.Services;

public static class ShlinksApiServiceCollectionExtensions
{
    public static IServiceCollection AddShlinksApi(
        this IServiceCollection services,
        IConfigurationSection configuration
    )
    {
        services.TryAddSingleton<IShlinksService, ShlinksService>();
        services.TryAddSingleton<IShlinksStatsService, ShlinksStatsService>();
        return services;
    }
}
