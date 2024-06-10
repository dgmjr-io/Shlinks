/*
 * LinkBase.cs
 *     Created: 2024-04-15T16:33:55-04:00
 *    Modified: 2024-05-04T14:52:14-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Models;

using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

public class LinkBase()
{
    [JProp("shortCode")]
    [JsonProperty("shortCode")]
    public virtual string Slug { get; set; }

    [JProp("longUrl")]
    [JsonProperty("longUrl")]
    [Required]
    [JsonConverter<JsonUriStringConverter>]
    public virtual Uri LongUrl { get; set; }

    [JProp("shortUrl")]
    [JsonProperty("shortUrl")]
    [Required]
    [JsonConverter<JsonUriStringConverter>]
    public virtual Uri ShortUrl { get; set; }

    [JProp("title")]
    [JsonProperty("title")]
    public virtual string Title { get; set; }

    [JProp("tags")]
    [JsonProperty("tags")]
    public virtual string[] Tags { get; set; }

    /// <summary>
    /// <see langword="true"/> if the link is crawlable (i.e., reachable by search engines); <see langword="false"/> otherwise.
    /// </summary>
    [JProp("crawlable")]
    [JsonProperty("crawlable")]
    public virtual bool IsCrawlable { get; set; } = true;

    [JProp("createdAt")]
    [JsonProperty("createdAt")]
    public virtual DateTimeOffset? CreatedAt { get; set; }

    [JProp("domain")]
    [JsonProperty("domain")]
    public virtual string? Domain => ShortUrl?.Host;
}
