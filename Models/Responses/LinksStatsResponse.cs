/*
 * LinksStatsResponse.cs
 *     Created: 2024-05-05T19:37:01-04:00
 *    Modified: 2024-05-05T19:37:01-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

using System.Collections.Generic;

using Microsoft.Azure.Cosmos.Core.Collections;

namespace Shlinks.Responses;

public record struct LinkStatsResponse
{
    public int TotalClicks { get; set; }
    public int HumanClicks { get; set; }
    public int TotalClicksChange { get; set; }
    public int HumanClicksChange { get; set; }

    public Interval Interval { get; set; }

    [JProp("country")]
    public List<TopCountry> Countries { get; set; }

    [JProp("city")]
    public List<TopCity> Cities { get; set; }

    [JProp("referrer")]
    public List<TopReferrer> Referrers { get; set; }

    [JProp("browser")]
    public List<TopBrowser> Browsers { get; set; }

    [JProp("os")]
    public List<TopOs> OSes { get; set; }

    [JProp("clickStatistics")]
    public ClickStatistics Clicks { get; set; }
}

public record struct ClickStatistics
{
    [JProp("datasets")]
    public List<ClickDataset> Datasets { get; set; }
}

public record struct ClickDataset
{
    public List<ClickData> Data { get; set; }
}

public record struct ClickData
{
    [JProp("x")]
    public datetime Timestamp { get; set; }

    [JProp("y")]
    public int Clicks { get; set; }
}

public record struct Interval
{
    [JProp("startDate")]
    public datetime? Start { get; set; }

    [JProp("endDate")]
    public datetime? End { get; set; }

    [JProp("previousStartDate")]
    public datetime? PreviousStart { get; set; }

    [JProp("prevEndDate")]
    public datetime? PreviousEnd { get; set; }
}
