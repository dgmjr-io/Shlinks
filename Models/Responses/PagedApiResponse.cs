/*
 * PagedApiResponse.cs
 *     Created: 2024-06-11T06:22:55-04:00
 *    Modified: 2024-06-11T06:22:55-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Responses;

using Newtonsoft.Json;

using Shlinks.Abstractions;

public abstract class PagedApiResponse<TItem> : IShlinksApiResponse
{
    [JProp("data")]
    [JsonProperty("data")]
    public List<TItem> Items { get; set; } = [];

    [JProp("pagination")]
    [JsonProperty("pagination")]
    public Pagination Pagination { get; set; } = new ();
}

public record struct Pagination
{
    [JProp("currentPage")]
    [JsonProperty("currentPage")]
    public int CurrentPage { get; set; }
    [JProp("pagesCount")]
    [JsonProperty("pagesCount")]
    public int TotalPages { get; set; }
    [JProp("itemsPerPage")]
    [JsonProperty("itemsPerPage")]
    public int PageSize { get; set; }
    [JProp("itemsInCurrentPage")]
    [JsonProperty("itemsInCurrentPage")]
    public int CurrentPageSize { get; set; }
    [JProp("totalItems")]
    [JsonProperty("totalItems")]
    public int TotalItems { get; set; }
}
