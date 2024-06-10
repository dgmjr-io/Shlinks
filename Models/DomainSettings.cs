/*
 * DomainSettings.cs
 *     Created: 2024-06-09T21:28:12-04:00
 *    Modified: 2024-06-09T21:28:13-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks;

public class DomainSettings
{
    public string Hostname { get; set; }
    public string[] DefaultTags { get; set; }
}
