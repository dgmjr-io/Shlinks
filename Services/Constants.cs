/*
 * Constants.cs
 *     Created: 2024-04-16T00:40:25-04:00
 *    Modified: 2024-04-23T15:25:25-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks;

public static class ApiConstants
{
    public const string ServiceName = "Shlinks";
    public const string BaseUriString = "https://api.Shlinks/api";
    public const string StatsBaseUriString = "https://statistics.Shlinks/statistics";
    public const string HttpDependencyName = "HTTP";

    public static class Routes
    {
        public const string Links = "links";
        public const string GetDomains = "api/domains";
        public const string Domains_Delete = "domains/delete/{0}";
        public const string Links_ById = $"{Links}/{{0}}";
        public const string GetLinks = $"api/{Links}";
        public const string Links_Stats =
            $"link/{{0}}?period={{1}}&tz={{2}}&clicksChartInterval={{3}}";
    }
}
