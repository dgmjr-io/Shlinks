/*
 * TopX.cs
 *     Created: 2024-05-05T23:12:57-04:00
 *    Modified: 2024-05-05T23:12:59-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Responses;

using System.ComponentModel.DataAnnotations;

public class TopCity() : TopX()
{
    [JProp("city")]
    public override string Code { get; set; }

    [JProp("name")]
    public override string Name { get; set; }

    [JProp("countryCode")]
    [MinLength(2), MaxLength(2), RegularExpression("^[A-Z]{2}$")]
    public virtual string CountryCode { get; set; }
}
