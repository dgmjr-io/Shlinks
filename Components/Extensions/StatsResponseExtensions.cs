/*
 * StatsResponseExtensions.cs
 *     Created: 2024-05-18T20:39:03-04:00
 *    Modified: 2024-05-18T20:39:03-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */


namespace Shlinks.Extensions;

using Shlinks.Responses;

public static class StatsResponseExtensions
{
    public static IActivity ToMessage(this LinkStatsResponse stats)
    {
        var message = MessageFactory.Text(stats.ToMarkdown());
        return message;
    }

    public static string ToMarkdown(this LinkStatsResponse stats)
    {
        return $"""
        **Total Clicks:** {stats.TotalClicks}

        **Human Clicks:** {stats.HumanClicks}

        **Total Clicks Change:** {stats.TotalClicksChange}

        **Human Clicks Change:** {stats.HumanClicksChange}

        **Top Countries:**
        {stats.Countries.ToMarkdown()}

        **Top Cities:**
        {stats.Cities.ToMarkdown()}

        **Top Referrers:**
        {stats.Referrers.ToMarkdown()}

        **Top Browsers:**
        {stats.Browsers.ToMarkdown()}

        **Top OSes:**
        {stats.OSes.ToMarkdown()}
        """;
    }

    public static string ToMarkdown<TTopX>(this IEnumerable<TTopX> topX)
        where TTopX : TopX
    {
        var sb = new StringBuilder();
        foreach (var item in topX ?? Empty<TTopX>())
        {
            sb.AppendLine($"- {item.Name}: {item.Score}");
        }
        return sb.ToString();
    }
}
