/*
 * DomainBase.cs
 *     Created: 2024-05-02T18:49:53-04:00
 *    Modified: 2024-05-02T18:49:53-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Models;

using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

using Shlinks.Models.Abstractions;

public record class Domain : IHaveJsonExtensionData
{
    [Required]
    [JProp("domain")]
    [JsonProperty("domain")]
    public string Hostname { get; set; }

    [JProp("regular404Redirect")]
    [JsonProperty("regular404Redirect")]
    [JsonConverter<JsonUriStringConverter>]
    public Uri? Redirect404 { get; set; }

    [JProp("baseUrlRedirect")]
    [JsonProperty("baseUrlRedirect")]
    [JsonConverter<JsonUriStringConverter>]
    public Uri? BaseUrlRedirect { get; set; }

    [JProp("invalidShortUrlRedirect")]
    [JsonProperty("invalidShortUrlRedirect")]
    [JsonConverter<JsonUriStringConverter>]
    public Uri? InvalidShortUrlRedirect { get; set; }

    [JsonExtensionData]
    public IDictionary<string, JElem> Properties { get; set; }
}
