/*
 * LinkStatsRequest.cs
 *     Created: 2024-05-05T19:17:58-04:00
 *    Modified: 2024-05-05T19:17:58-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */


namespace Shlinks.Requests;

using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using AdaptiveExpressions;

public class GetLinkStatsRequest
{
    [JIgnore]
    public string? LinkId { get; set; }

    [EnumDataType(typeof(TimeZone))]
    [JProp("tz")]
    [JsonConverter<TimeZoneInfoConverter>]
    public TimeZoneInfo? TimeZone { get; set; } = TimeZoneInfo.Local;

    [EnumDataType(typeof(Period))]
    [JProp("period")]
    [JsonConverter<EnumMemberValueToStringConverter<Period>>]
    public Period Period { get; set; } = Period.AllTime;

    [JProp("clicksChartInterval")]
    public ClicksChartInterval? ClicksChartInterval { get; set; }
}

public enum Period
{
    [Display(Name = "All Time", ShortName = "total")]
    [EnumMember(Value = "total")]
    AllTime = 0,

    [Display(Name = "Last 7 Days", ShortName = "last7")]
    [EnumMember(Value = "last7")]
    Last7 = 7,

    [Display(Name = "Last 30 Days", ShortName = "last30")]
    [EnumMember(Value = "last30")]
    Last30 = 30,

    [Display(Name = "Last month", ShortName = "lastmonth")]
    [EnumMember(Value = "lastmonth")]
    LastMonth = 31,

    [Display(Name = "Last week", ShortName = "week")]
    [EnumMember(Value = "week")]
    LastWeek = -7,

    [Display(Name = "Yesterday", ShortName = "yesterday")]
    [EnumMember(Value = "yesterday")]
    Yesterday = -1
}

public enum ClicksChartInterval
{
    [Display(Name = "Hourly")]
    [EnumMember(Value = "hour")]
    Hour,

    [Display(Name = "Daily")]
    [EnumMember(Value = "day")]
    Day,

    [Display(Name = "Weekly")]
    [EnumMember(Value = "week")]
    Week,

    [Display(Name = "Monthly")]
    [EnumMember(Value = "month")]
    Month
}

public enum TimeZone
{
    [Display(Name = "Coordinated Universal Time")]
    [EnumMember(Value = "UTC")]
    UTC,

    [Display(Name = "Eastern Standard Time")]
    [EnumMember(Value = "EST5EDT")]
    EST,

    [Display(Name = "Central Standard Time")]
    [EnumMember(Value = "CST6CDT")]
    CST,

    [Display(Name = "Mountain Standard Time")]
    [EnumMember(Value = "MST7MDT")]
    MST,

    [Display(Name = "Pacific Standard Time")]
    [EnumMember(Value = "PST8PDT")]
    PST
}
