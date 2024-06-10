/*
 * ApiVersion.cs
 *     Created: 2024-06-10T21:50:17-04:00
 *    Modified: 2024-06-10T21:50:17-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

using System.ComponentModel.DataAnnotations;

namespace Shlinks;

public enum ApiVersion
{
    [Display(Name = "1")]
    [EnumValue("1")]
    V1 = 1,

    [Display(Name = "2")]
    [EnumValue("2")]
    V2 = 2,

    [Display(Name = "3")]
    [EnumValue("3")]
    V3 = 3,

    Latest = V3
}
