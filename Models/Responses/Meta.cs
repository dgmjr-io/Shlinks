/*
 * Meta.cs
 *     Created: 2024-06-11T02:12:53-04:00
 *    Modified: 2024-06-11T02:12:54-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

using Newtonsoft.Json;

namespace Shlinks.Models;

public record struct Meta
{
    [JProp("validSince")]
    [JsonProperty("validSince")]
    public DateTimeOffset? ValidFrom { get; init; }

    [JProp("validUntil")]
    [JsonProperty("validUntil")]
    public DateTimeOffset? ValidTo { get; init; }

    [JProp("maxVisits")]
    [JsonProperty("maxVisits")]
    public int? MaximumVisits { get; init; }
}
