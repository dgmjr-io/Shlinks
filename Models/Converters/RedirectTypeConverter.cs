/*
 * RedirectTypeConverter.cs
 *     Created: 2024-04-27T11:17:44-04:00
 *    Modified: 2024-04-27T11:17:45-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks;

using System;
using System.Text.Json;

using Shlinks.Models;

public class RedirectTypeConverter : JsonConverter<RedirectType>
{
    public override RedirectType Read(ref Utf8JsonReader reader, type typeToConvert, Jso options)
    {
        if (reader.TokenType is not JTokenType.String or JTokenType.Number)
        {
            throw new JException(
                $"Unexpected token parsing RedirectType. Expected String or Number, got {reader.TokenType}"
            );
        }

        var value = reader.GetString();

        return value switch
        {
            "301" => RedirectType.Permanent,
            "302" => RedirectType.Temporary,
            "303" => RedirectType.SeeOther,
            "307" => RedirectType.TemporaryRedirect,
            "308" => RedirectType.PermanentRedirect,
            _ => throw new JException(),
        };
    }

    public override void Write(Utf8JsonWriter writer, RedirectType value, Jso options)
    {
        writer.WriteStringValue(
            value switch
            {
                RedirectType.Permanent => "301",
                RedirectType.Temporary => "302",
                RedirectType.SeeOther => "303",
                RedirectType.TemporaryRedirect => "307",
                RedirectType.PermanentRedirect => "308",
                _ => throw new JException(),
            }
        );
    }
}
