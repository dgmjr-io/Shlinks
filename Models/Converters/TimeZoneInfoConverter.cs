/*
 * TimeZoneInfoConverter.cs
 *     Created: 2024-05-19T18:19:03-04:00
 *    Modified: 2024-05-19T18:19:03-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

using System.Text.Json;

namespace Shlinks;

public class TimeZoneInfoConverter : JsonConverter<TimeZoneInfo>
{
    public override TimeZoneInfo? Read(ref Utf8JsonReader reader, type typeToConvert, Jso options)
    {
        if (reader.TokenType is not JTokenType.String)
        {
            throw new JException(
                $"Unexpected token parsing TimeZoneInfo. Expected String, got {reader.TokenType}"
            );
        }

        var value = reader.GetString();

        return TimeZoneInfo.FindSystemTimeZoneById(value);
    }

    public override void Write(Utf8JsonWriter writer, TimeZoneInfo value, Jso options)
    {
        writer.WriteStringValue(value.Id);
    }
}
