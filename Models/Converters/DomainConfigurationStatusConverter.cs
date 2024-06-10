/*
 * DomainConfigurationStatusConverter.cs
 *     Created: 2024-05-02T19:05:03-04:00
 *    Modified: 2024-05-02T19:05:04-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

using System;
using System.Text.Json;

namespace Shlinks;

[Obsolete("The DomainConfigurationStatus enum is not supported by the shlinks.io API", true)]
public class DomainConfigurationStatusConverter : JsonConverter<DomainConfigurationStatus>
{
    public override DomainConfigurationStatus Read(
        ref Utf8JsonReader reader,
        type typeToConvert,
        Jso options
    )
    {
        if (reader.TokenType is not JTokenType.String or JTokenType.Number)
        {
            throw new JException(
                $"Unexpected token parsing DomainConfigurationStatus. Expected String or Number, got {reader.TokenType}"
            );
        }

        var value = reader.GetString();

        return value switch
        {
            "not_configured" => DomainConfigurationStatus.NotConfigured,
            "configured" => DomainConfigurationStatus.Configured,
            "not_registered" => DomainConfigurationStatus.NotRegistered,
            "extra_records" => DomainConfigurationStatus.ExtraRecords,
            _ => throw new JException(),
        };
    }

    public override void Write(Utf8JsonWriter writer, DomainConfigurationStatus value, Jso options)
    {
        writer.WriteStringValue(
            value switch
            {
                DomainConfigurationStatus.NotConfigured => "not_configured",
                DomainConfigurationStatus.Configured => "configured",
                DomainConfigurationStatus.NotRegistered => "not_registered",
                DomainConfigurationStatus.ExtraRecords => "extra_records",
                _ => throw new JException(),
            }
        );
    }
}
