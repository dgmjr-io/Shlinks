/*
 * ShlinksService.cs
 *     Created: 2024-04-15T07:11:50-04:00
 *    Modified: 2024-04-23T15:24:31-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Services;

using System.IO;
using System.Net.Http;
using System.Net.Http.Json;

using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc;

using TelemetryConstants = Dgmjr.BotFramework.TelemetryConstants;

public class ServiceBase(
    IOptions<Settings> settings,
    ILogger logger,
    IHttpClientFactory clientFactory,
    IOptions<JsonOptions> jsonOptions,
    IBotTelemetryClient telemetryClient
)
{
    private readonly Jso _jso = new(jsonOptions.Value.JsonSerializerOptions);

    protected virtual async Task<TResponse?> GetAsync<TResponse>(
        string path,
        object? request = default
    )
    {
        var sw = Stopwatch.StartNew();
        HttpResponseMessage? response = default;
        var requestUri = request is null
            ? Path.Join(BaseUri.ToString(), path)
            : Path.Join(BaseUri.ToString(), path) + "?" + request.ToQueryString();

        var telemetry = new DependencyTelemetry
        {
            Name = $"{HttpMethod.Get} {requestUri}",
            Data = requestUri,
            Timestamp = DateTimeOffset.Now,
            Type = TelemetryConstants.HttpRequest
        };

        using var operation = (telemetryClient as IDmBotTelemetryClient)?.StartOperation(telemetry);

        try
        {
            using var client = NewClient();
            response = (await client.GetAsync(requestUri)).EnsureSuccessStatusCode();
            telemetry.Success = response.IsSuccessStatusCode;
            var responseObject = await response.Content.ReadFromJsonAsync<TResponse>(_jso);
            logger.RequestSucceeded(HttpMethod.Get, path, sw.Elapsed);
            return responseObject;
        }
        catch (Exception ex)
        {
            logger.RequestFailed(HttpMethod.Get, path, ex);
            telemetry.Success = false;
            return default;
        }
        finally { }
    }

    protected virtual async Task<TResponse?> PostAsync<TRequest, TResponse>(
        string? path,
        TRequest? postBody
    )
    {
        var sw = Stopwatch.StartNew();
        HttpResponseMessage? response = default;
        try
        {
            using var client = NewClient();
            response = (
                await client.PostAsJsonAsync(Path.Join(BaseUri.ToString(), path), postBody, _jso)
            ).EnsureSuccessStatusCode();
            var responseObject = await response.Content.ReadFromJsonAsync<TResponse>(_jso);
            logger.RequestSucceeded(HttpMethod.Post, path, sw.Elapsed);
            return responseObject;
        }
        catch (Exception ex)
        {
            logger.RequestFailed(HttpMethod.Post, path, ex);
            telemetryClient.TrackException(ex);
            return default;
        }
        finally
        {
            telemetryClient.TrackDependency(
                ApiConstants.HttpDependencyName,
                path,
                HttpMethod.Post.Method,
                await (response?.Content.ReadAsStringAsync() ?? Task.FromResult("None")),
                datetime.Now - sw.Elapsed,
                sw.Elapsed,
                response?.StatusCode.ToString() ?? "None",
                response?.IsSuccessStatusCode ?? false
            );
        }
    }

    protected async Task DeleteAsync(string path)
    {
        var sw = Stopwatch.StartNew();
        HttpResponseMessage? response = default;
        try
        {
            using var client = NewClient();
            response = (
                await client.DeleteAsync(Path.Join(BaseUri.ToString(), path))
            ).EnsureSuccessStatusCode();
            logger.RequestSucceeded(HttpMethod.Delete, path, sw.Elapsed);
        }
        catch (Exception ex)
        {
            logger.RequestFailed(HttpMethod.Delete, path, ex);
            telemetryClient.TrackException(ex);
        }
        finally
        {
            telemetryClient.TrackDependency(
                ApiConstants.HttpDependencyName,
                path,
                HttpMethod.Delete.Method,
                await (response?.Content.ReadAsStringAsync() ?? Task.FromResult("None")),
                datetime.Now - sw.Elapsed,
                sw.Elapsed,
                response?.StatusCode.ToString() ?? "None",
                response?.IsSuccessStatusCode ?? false
            );
        }
    }

    protected virtual Uri BaseUri => settings.Value.BaseUrl;

    protected virtual HttpClient NewClient() => clientFactory.CreateClient(nameof(ShlinksService));
}
