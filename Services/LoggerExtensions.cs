/*
 * LoggerExtensions.cs
 *     Created: 2024-04-17T09:50:37-04:00
 *    Modified: 2024-04-23T15:29:05-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Api;

using System.Net.Http;

public static partial class LoggerExtensions
{
    [LoggerMessage(
        EventId = 1000,
        Level = LogLevel.Error,
        Message = "Failed to create link: {link}"
    )]
    public static partial void LinkCreationFailed(this ILogger logger, string link, Exception ex);

    [LoggerMessage(
        EventId = 1001,
        Level = LogLevel.Error,
        Message = "Failed to create domain: {domain}"
    )]
    public static partial void DomainCreationFailed(
        this ILogger logger,
        string domain,
        Exception ex
    );

    [LoggerMessage(
        EventId = 1002,
        Level = LogLevel.Error,
        Message = "Failed to delete domain: {DomainID}"
    )]
    public static partial void DomainDeletionFailed(
        this ILogger logger,
        int domainId,
        Exception ex
    );

    [LoggerMessage(
        EventId = 1003,
        Level = LogLevel.Error,
        Message = "Failed to {method} to {path}"
    )]
    public static partial void RequestFailed(
        this ILogger logger,
        HttpMethod method,
        string path,
        Exception ex
    );

    [LoggerMessage(
        EventId = 1004,
        Level = LogLevel.Information,
        Message = "Request to {method} {path} succeeded in {elapsed}"
    )]
    public static partial void RequestSucceeded(
        this ILogger logger,
        HttpMethod method,
        string path,
        TimeSpan elapsed
    );

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
}
