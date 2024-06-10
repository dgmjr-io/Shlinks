/*
 * CreateLinkRequest.cs
 *
 *   Created: 2024-45-16T00:45:22-04:00
 *   Modified: 2024-10-17T10:10:09-04:00
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Requests;

using System.ComponentModel.DataAnnotations;
using System.Text.Json;

using MediatR;

using Newtonsoft.Json;

using Shlinks.Abstractions;
using Shlinks.Models;
using Shlinks.Models.Abstractions;
using Shlinks.Responses;

public class CreateOrUpdateLinkRequest : LinkBase, IHaveJsonExtensionData
{
    [JProp("customSlug")]
    [JsonProperty("customSlug")]
    public override string Slug
    {
        get => base.Slug;
        set => base.Slug = value;
    }

    [JsonExtensionData]
    public IDictionary<string, JElem> Properties { get; set; }

    [JIgnore]
    [JsonIgnore]
    public ApiVersion ApiVersion { get; set; } = ApiVersion.Latest;
}
