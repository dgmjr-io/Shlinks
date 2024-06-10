/*
 * IHaveJsonExtensionData.cs
 *     Created: 2024-05-01T02:39:46-04:00
 *    Modified: 2024-05-01T02:39:47-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Models.Abstractions;

public interface IHaveJsonExtensionData
{
    [JsonExtensionData]
    public IDictionary<string, JElem> Properties { get; set; }
}
