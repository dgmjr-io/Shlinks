/*
 * LinkSource.cs
 *     Created: 2024-05-01T02:51:03-04:00
 *    Modified: 2024-05-01T02:51:03-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks;
using System.Runtime.Serialization;

public enum LinkSource
{
    [EnumMember(Value = "api")]
    Api,

    [EnumMember(Value = "website")]
    Website
}
