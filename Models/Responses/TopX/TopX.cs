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

using Humanizer;

public abstract class TopX()
{
    public abstract string Code { get; set; }

    [JIgnore]
    public virtual string Name
    {
        get => Code;
        set => Code = value;
    }

    [JProp("score")]
    public virtual int Score { get; set; }

    // private T? TryGetValue<T>(string key) =>
    //     TryGetValue(key, out var value) && value is JElem jelement
    //         ? jelement.Deserialize<T>()
    //         : default;

    // private string GetJsonPropertyName([CallerMemberName] string? propertyName = default) =>
    //     propertyName is nameof(Code)
    //         ? GetType().Name.Replace("Top", string.Empty).ToLower()
    //         : propertyName.Camelize();
}
