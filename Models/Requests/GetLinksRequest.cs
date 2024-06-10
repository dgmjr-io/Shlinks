/*
 * GetLinksRequest.cs
 *     Created: 2024-04-27T17:37:16-04:00
 *    Modified: 2024-04-27T17:37:16-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Requests;

using System.ComponentModel.DataAnnotations;

public class GetLinksRequest() : QueryRequest<GetLinksResponse>
{
    public GetLinksRequest(int domainId, string? username = default)
        : this()
    {
        Username = username;
        DomainId = domainId;
    }

    public string? Username { get; set; } = default;

    [Required]
    [JProp("domain_id")]
    public int DomainId { get; set; }

    [Range(1, 150)]
    public int Limit { get; set; } = 150;

    public SortOrder SortOrder { get; set; } = SortOrder.Descending;

    public string? PageToken { get; set; } = default;
}
