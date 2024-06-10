/*
 * VisitsSummary.cs
 *     Created: 2024-06-11T02:11:18-04:00
 *    Modified: 2024-06-11T02:11:18-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

using Newtonsoft.Json;

namespace Shlinks.Models;

public record struct VisitsSummary
{
    [JProp("total")]
    [JsonProperty("total")]
    public int TotalVisits { get; init; }

    [JProp("nonBots")]
    [JsonProperty("nonBots")]
    public int NonBotVisits { get; init; }

    [JProp("bots")]
    [JsonProperty("bots")]
    public int BotVisits { get; init; }
}
