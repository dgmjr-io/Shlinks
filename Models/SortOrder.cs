/*
 * SortOrder.cs
 *     Created: 2024-04-27T18:32:27-04:00
 *    Modified: 2024-04-27T18:32:27-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Models;

using System.Runtime.Serialization;

public enum SortOrder
{
    [EnumMember(Value = "asc")]
    Ascending,

    [EnumMember(Value = "desc")]
    Descending
}
