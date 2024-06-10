/*
 * LoggingExtensions.cs
 *
 *   Created: 2024-07-18T18:07:52-04:00
 *   Modified: 2024-07-18T18:07:52-04:00
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks;

public static partial class LoggingExtensions
{
    [LoggerMessage(
        0,
        LogLevel.Information,
        "Logging is bootstrapped.",
        EventName = "LoggingIsBootstrapped"
    )]
    public static partial void LoggingIsBootstrapped(this ILogger logger);

    [LoggerMessage(
        1,
        LogLevel.Information,
        "Logger is bootstrapped.",
        EventName = "LoggerIsBootstrapped"
    )]
    public static partial void LoggerIsBootstrapped(this ILogger logger);

    [LoggerMessage(
        2,
        LogLevel.Information,
        "Configuring {Service} in {Environment}...",
        EventName = "ConfiguringService"
    )]
    public static partial void ConfiguringService(
        this ILogger logger,
        string service,
        string? environment
    );

    [LoggerMessage(
        2,
        LogLevel.Information,
        "Configuring Application {Service} in {Environment}...",
        EventName = "ConfiguringService"
    )]
    public static partial void ConfiguringApp(
        this ILogger logger,
        string service,
        string? environment
    );
}
