/*
 * ShlinksStatsService.cs
 *     Created: 2024-05-08T08:36:05-04:00
 *    Modified: 2024-05-08T08:36:06-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Services;

using Shlinks.Responses;

public interface IShlinksStatsService
{
    Task<LinkStatsResponse> GetLinkStatsAsync(
        string linkId,
        Period period = 0,
        TimeZoneInfo? timeZone = default,
        ClicksChartInterval clicksChartInterval = ClicksChartInterval.Hour
    );
}
