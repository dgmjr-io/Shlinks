/*
 * Constants.cs
 *     Created: 2024-04-16T15:28:11-04:00
 *    Modified: 2024-05-15T20:07:56-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks;

public static class Constants
{
    public const string Namespace = nameof(Shlinks);
    public const string DefaultSettingsPath = "=settings[\"Shlinks\"]";
    public const string DefaultBaseUrlPath = DefaultSettingsPath + ".baseUrl";
    public const string DefaultApiKeyPath = DefaultSettingsPath + ".apiKey";
    public const string DefaultLinkAdministratorsPath = DefaultSettingsPath + ".linkAdministrators";
    public const string DefaultDomainPath = DefaultSettingsPath + ".defaultDomain";

    public static readonly StrExp DefaultSettingsExpression = new(DefaultSettingsPath);
    public static readonly StrExp DefaultBaseUrlExpression = new(DefaultBaseUrlPath);
    public static readonly StrExp DefaultApiKeyExpression = new(DefaultApiKeyPath);
    public static readonly ArrayExpression<string> DefaultLinkAdministratorsExpression =
        new(DefaultLinkAdministratorsPath);
    public static readonly StrExp DefaultDomainExpression = new(DefaultDomainPath);
}
