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

using Azure.Core;

using Microsoft.AspNetCore.Mvc;

using Shlinks.Models;
using Shlinks.Requests;
using Shlinks.Responses;
using Shlinks.Services;

public partial interface IShlinksService { }

public class ShlinksService(
    IOptions<Settings> settings,
    ILogger<ShlinksService> logger,
    IHttpClientFactory clientFactory,
    IOptions<JsonOptions> jsonOptions,
    IBotTelemetryClient telemetryClient
) : ServiceBase(settings, logger, clientFactory, jsonOptions, telemetryClient), IShlinksService
{
    public ShlinksService(string baseUrl, string apiKey, ILogger<ShlinksService> logger, IHttpClientFactory clientFactory, IOptions<JsonOptions> jsonOptions, IBotTelemetryClient telemetryClient) : this(
        new OptionsWrapper<Settings>(new Settings { BaseUrl = new Uri(baseUrl), ApiKey = apiKey }),
        logger,
        clientFactory,
        jsonOptions,
        telemetryClient
    )
    { }
    public ShlinksService(string baseUrl, string apiKey, ILogger<ShlinksService> logger, IHttpClientFactory clientFactory, JsonOptions jsonOptions, IBotTelemetryClient telemetryClient) : this(
        new OptionsWrapper<Settings>(new Settings { BaseUrl = new Uri(baseUrl), ApiKey = apiKey }),
        logger,
        clientFactory,
        new OptionsWrapper<JsonOptions>(jsonOptions),
        telemetryClient
    )
    { }


    // [HttpPost(ApiConstants.Routes.GetDomains)]
    // public virtual async Task<Domain?> CreateDomainAsync(string domain)
    // {
    //     var request = new CreateDomainRequest { Hostname = domain };
    //     return await PostAsync<CreateDomainRequest, Domain>(ApiConstants.Routes.GetDomains, request);
    // }

    // [Obsolete("This operation isn't supported by the shlinks.io API.", true)]
    // [HttpPost(ApiConstants.Routes.GetDomains)]
    // public virtual async Task<Domain?> CreateDomainAsync(CreateDomainRequest createDomainRequest)
    // {
    //     return await PostAsync<CreateDomainRequest, Domain>(
    //         ApiConstants.Routes.GetDomains,
    //         createDomainRequest
    //     );
    // }

    [HttpPost(ApiConstants.Routes.Links)]
    public virtual async Task<Link?> CreateLinkAsync(
        Uri url,
        string? domain = default,
        string? slug = default,
        params string[] tags
    )
    {
        if (!url.IsAbsoluteUri)
        { throw new ArgumentException("The URL must be an absolute URI.", nameof(url)); }

        if(!settings.Value.Domains.ContainsKey(domain ?? settings.Value.DefaultDomain))
        { throw new KeyNotFoundException($"Domain '{domain}' not found."); }

        var request = new CreateOrUpdateLinkRequest
        {
            Url = url,
            Domain = domain ?? settings.Value.DefaultDomain,
            // TimeToLive = settings.Value.DefaultTimeToLive,
            Slug = slug,
            Tags = [.. settings.Value.Domains[domain ?? settings.Value.DefaultDomain].DefaultTags, ..tags],
        };
        return await PostAsync<CreateLinkRequest, Link>(ApiConstants.Routes.Links, request);
    }

    [HttpPost(ApiConstants.Routes.Links)]
    public virtual async Task<Link?> CreateLinkAsync(CreateOrUpdateLinkRequest createLinkRequest)
    {
        createLinkRequest.Domain ??= settings.Value.DefaultDomain;
        createLinkRequest.Tags ??= settings.Value.DefaultTags[createLinkRequest.Domain];
        return await PostAsync<CreateLinkRequest, Link>(
            ApiConstants.Routes.Links,
            createLinkRequest
        );
    }

    [HttpPost(ApiConstants.Routes.Domains_Delete)]
    public virtual async Task<bool> DeleteDomainAsync(int domainId)
    {
        var response = await PostAsync<object, ApiResponse>(
            default,
            Format(ApiConstants.Routes.Domains_Delete, domainId)
        );
        return response?.Success ?? false;
    }

    [HttpPost(ApiConstants.Routes.Links_ById)]
    public virtual async Task DeleteLinkAsync(string linkId)
    {
        await DeleteAsync(Format(ApiConstants.Routes.Links_ById, linkId));
    }

    // public virtual async Task<GetLinksBasicResponse> GetMyLinksBasicAsync(string domain, string? username = default)
    // {
    //     return await GetMyLinksAsync(domain, username);
    // }

    public virtual async Task<Link?> UpdateLinkAsync(Link link)
    {
        return await PostAsync<Link, Link>(
            Format(ApiConstants.Routes.Links_ById, link.Id),
            link
        );
    }

    public virtual async Task<ListDomainsResponse> GetDomainsAsync()
    {
        return (await GetAsync<ListDomainsResponse>(ApiConstants.Routes.GetDomains))!;
    }

    public virtual async Task<GetLinksResponse> GetMyLinksAsync(string domain, string? username = default)
    {
        var domains = (await GetDomainsAsync()).ToDictionary();
        var domainId = domains.TryGetValue(domain, out var d) ? d.Id : throw new KeyNotFoundException($"Domain '{domain}' not found");

        var request = new GetLinksRequest(domainId, username);
        var response = await GetAsync<GetLinksResponse>(
            ApiConstants.Routes.GetLinks,
            request
        );

        if (username is not null && response.Links is not null)
            response.Links = response.Links
                .Where(l => l.Tags.Contains(username))
                .ToList();

        return response;
    }
}
