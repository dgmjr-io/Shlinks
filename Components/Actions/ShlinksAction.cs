/*
 * ShlinksAction.cs
 *     Created: 2024-04-27T17:19:37-04:00
 *    Modified: 2024-04-27T17:19:37-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Components.Actions;

using System.Net.Http;

using Microsoft.AspNetCore.Mvc;

using Shlinks.Services;

public abstract class ShlinksAction<TAction>(
    string kind,
    IServiceProvider services,
    ILoggerFactory loggerFactory,
    [CallerFilePath] string sourceFilePath = "",
    [CallerLineNumber] int sourceLineNumber = 0
) : BotCustomComponentAction<TAction>(kind, sourceFilePath, sourceLineNumber), ILog
    where TAction : ShlinksAction<TAction>
{
    public ILogger Logger { get; private set; } = loggerFactory?.CreateLogger<TAction>();
    private ILoggerFactory LoggerFactory { get; set; } = loggerFactory;

    [JsonProperty("baseUrl")]
    public StrExp BaseUrl { get; set; } = new(Settings.DefaultBaseUrl);

    [JsonProperty("apiKey")]
    public StrExp ApiKey { get; set; }

    private IShlinksService? _ShlinksService = default;
    private IShlinksStatsService? _ShlinksStatsService = default;

    protected IShlinksService GetShlinksService(DialogContext dc)
    {
        return _ShlinksService ??=
            services?.GetService<IShlinksService>()
            ?? dc.Context.TurnState.Get<IShlinksService>()
            ?? GetShlinksServiceInternal(dc);
    }

    protected IShlinksStatsService GetShlinksStatsService(DialogContext dc)
    {
        return _ShlinksStatsService ??=
            services?.GetService<IShlinksStatsService>()
            ?? dc.Context.TurnState.Get<IShlinksStatsService>()
            ?? GetShlinksStatsServiceInternal(dc);
    }

    private IShlinksService GetShlinksServiceInternal(DialogContext dc)
    {
        LoggerFactory ??= dc.Context.TurnState.Get<ILoggerFactory>();
        LoggerFactory.ThrowIfNull();

        var baseUrl = dc.State.GetStringValue(BaseUrl.GetValue(dc.State));
        baseUrl.ThrowIfNullOrEmpty();

        var apiKey = dc.State.GetStringValue(ApiKey.GetValue(dc.State));
        apiKey.ThrowIfNullOrEmpty();

        var logger = LoggerFactory.CreateLogger<ShlinksService>();
        logger.ThrowIfNull();

        var httpClientFactory = dc.Context.TurnState.Get<IHttpClientFactory>();
        httpClientFactory.ThrowIfNull();

        var jsonOptions = dc.Context.TurnState.Get<IOptions<JsonOptions>>();
        jsonOptions.ThrowIfNull();

        var telemetryClient = dc.Context.TurnState.Get<IBotTelemetryClient>();
        telemetryClient.ThrowIfNull();

        return _ShlinksService ??= new ShlinksService(
            baseUrl,
            apiKey,
            logger,
            httpClientFactory,
            jsonOptions,
            telemetryClient
        );
    }

    private IShlinksStatsService GetShlinksStatsServiceInternal(DialogContext dc)
    {
        LoggerFactory ??= dc.Context.TurnState.Get<ILoggerFactory>();
        LoggerFactory.ThrowIfNull();

        var baseUrl = BaseUrl.GetValue(dc.State);
        baseUrl.ThrowIfNullOrEmpty();

        var apiKey = ApiKey.GetValue(dc.State);
        apiKey.ThrowIfNullOrEmpty();

        var logger = LoggerFactory.CreateLogger<ShlinksStatsService>();
        logger.ThrowIfNull();

        var httpClientFactory = dc.Context.TurnState.Get<IHttpClientFactory>();
        httpClientFactory.ThrowIfNull();

        var jsonOptions = dc.Context.TurnState.Get<IOptions<JsonOptions>>();
        jsonOptions.ThrowIfNull();

        var telemetryClient = dc.Context.TurnState.Get<IBotTelemetryClient>();
        telemetryClient.ThrowIfNull();

        return _ShlinksStatsService ??= new ShlinksStatsService(
            baseUrl,
            apiKey,
            logger,
            httpClientFactory,
            jsonOptions,
            telemetryClient
        );
    }
}
