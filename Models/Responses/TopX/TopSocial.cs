/*
 * TopX.cs
 *     Created: 2024-05-05T23:12:57-04:00
 *    Modified: 2024-05-05T23:12:59-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Responses;

public class TopSocial() : TopX()
{
    [JProp("social")]
    public override string Code { get; set; }
}
