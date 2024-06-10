/*
 * ShlinksStatsService.cs
 *     Created: 2024-05-08T08:36:05-04:00
 *    Modified: 2024-05-08T08:36:06-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Services;

using System.Net.Http;

using Microsoft.AspNetCore.Mvc;

using Shlinks.Responses;

public class ShlinksStatsService(
    IOptions<Settings> settings,
    ILogger<ShlinksStatsService> logger,
    IHttpClientFactory clientFactory,
    IOptions<JsonOptions> jsonOptions,
    IBotTelemetryClient telemetryClient
) : ServiceBase(settings, logger, clientFactory, jsonOptions, telemetryClient), IShlinksStatsService
{
    public ShlinksStatsService(
        string baseUrl,
        string apiKey,
        ILogger<ShlinksStatsService> logger,
        IHttpClientFactory clientFactory,
        IOptions<JsonOptions> jsonOptions,
        IBotTelemetryClient telemetryClient
    )
        : this(
            new OptionsWrapper<Settings>(
                new Settings { BaseUrl = new Uri(baseUrl), ApiKey = apiKey }
            ),
            logger,
            clientFactory,
            jsonOptions,
            telemetryClient
        ) { }

    public ShlinksStatsService(
        string baseUrl,
        string apiKey,
        ILogger<ShlinksStatsService> logger,
        IHttpClientFactory clientFactory,
        JsonOptions jsonOptions,
        IBotTelemetryClient telemetryClient
    )
        : this(
            new OptionsWrapper<Settings>(new() { BaseUrl = new Uri(baseUrl), ApiKey = apiKey }),
            logger,
            clientFactory,
            new OptionsWrapper<JsonOptions>(jsonOptions),
            telemetryClient
        ) { }

    public virtual async Task<LinkStatsResponse> GetLinkStatsAsync(
        string linkId,
        Period period = default,
        TimeZoneInfo? timeZone = default,
        ClicksChartInterval clicksChartInterval = default
    )
    {
        timeZone ??= TimeZoneInfo.Local;
        var request = new GetLinkStatsRequest
        {
            LinkId = linkId,
            Period = period,
            TimeZone = timeZone,
            ClicksChartInterval = clicksChartInterval,
        };
        return (
            await GetAsync<LinkStatsResponse>(
                Format(
                    ApiConstants.Routes.Links_Stats,
                    linkId,
                    period.GetShortName(),
                    timeZone.Id,
                    clicksChartInterval.GetEnumMemberValue()
                ) /*,
                request*/
            )
        )!;
    }

    protected override Uri BaseUri => settings.Value.StatsBaseUrl;

    protected override HttpClient NewClient()
    {
        var client = clientFactory.CreateClient(nameof(ShlinksStatsService));
        client.BaseAddress = settings.Value.StatsBaseUrl;
        return client;
    }
}
