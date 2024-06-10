/*
 * Configure.Logging.cs
 *
 *   Created: 2024-34-17T22:34:14-04:00
 *   Modified: 2024-34-17T22:34:14-04:00
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */


namespace Shlinks.Bot.Configure;

using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.AspNetCore;
using Serilog.Settings.Configuration;

public class Logging : Microsoft.AspNetCore.Builder.ConfiguratorBase<Logging>
{
    protected override void ConfigureLogging(ILoggingBuilder builder)
    {
        base.ConfigureLogging(builder);
        // (base.Context as IHostBuilder)?.UseSerilog(
        //     (hostingContext, loggerConfiguration) =>
        //     {
        //         loggerConfiguration.ReadFrom
        //             .Configuration(hostingContext.Configuration)
        //             .Enrich.FromLogContext()
        //             .WriteTo.Console();
        //     }
        // );
        // Log.Logger = new LoggerConfiguration().ReadFrom
        //     .Configuration(
        //         Configuration!,
        //         new ConfigurationReaderOptions { SectionName = nameof(Serilog) }
        //     )
        //     .CreateBootstrapLogger();
        Logger.LoggerIsBootstrapped();
    }
}
