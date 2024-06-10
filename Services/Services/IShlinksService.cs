/*
 * IShlinksService.cs
 *     Created: 2024-04-15T17:40:17-04:00
 *    Modified: 2024-04-23T15:25:03-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Services;

using Shlinks.Responses;

public partial interface IShlinksService
{
    // Task<Domain?> CreateDomainAsync(CreateDomainRequest createDomainRequest);

    Task<Link?> CreateLinkAsync(CreateOrUpdateLinkRequest createLinkRequest);

    Task<Link?> CreateLinkAsync(
        Uri url,
        string? domain = default,
        string? slug = default,
        params string[] tags
    );

    Task DeleteLinkAsync(string linkId);
    Task<bool> DeleteDomainAsync(int domainId);

    Task<GetLinksResponse> GetMyLinksAsync(string domain, string? username = default);

    // Task<GetLinksBasicResponse> GetMyLinksBasicAsync(string domain, string? username = default);

    // Task<Domain?> CreateDomainAsync(string domain);

    Task<ListDomainsResponse> GetDomainsAsync();

    Task<Link?> UpdateLinkAsync(Link link);
}
