/*
 * TopXConverter.cs
 *     Created: 2024-05-29T18:41:32-04:00
 *    Modified: 2024-05-29T18:41:32-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks;

using System;
using System.Text.Json;

using Shlinks.Responses;

public class TopXConverter<TTopX> : JsonConverter<TTopX>
    where TTopX : TopX
{
    public override TTopX? Read(ref Utf8JsonReader reader, type typeToConvert, Jso options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, TTopX value, Jso options)
    {
        throw new NotImplementedException();
    }
}
