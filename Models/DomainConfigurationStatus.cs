/*
 * DomainConfigurationStatus.cs
 *     Created: 2024-05-02T18:41:55-04:00
 *    Modified: 2024-05-02T18:41:55-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

using System.Runtime.Serialization;

namespace Shlinks.Models;

[Obsolete("This enum is not suported by the shlinks.io API.", true)]
public enum DomainConfigurationStatus
{
    [EnumMember(Value = "not_configured")]
    NotConfigured = 0,

    [EnumMember(Value = "configured")]
    Configured = 1,

    [EnumMember(Value = "not_registered")]
    NotRegistered = 2,

    [EnumMember(Value = "extra_records")]
    ExtraRecords = 3,
}
