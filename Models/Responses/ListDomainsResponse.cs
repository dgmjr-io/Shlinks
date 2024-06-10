/*
 * ListDomainsResponse.cs
 *     Created: 2024-05-01T02:34:40-04:00
 *    Modified: 2024-05-01T02:34:40-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Responses;

using Microsoft.Bot.Builder.Dialogs.Adaptive.Input;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;

using Shlinks.Models.Abstractions;

public class ListDomainsResponse(IEnumerable<Domain> domains) : List<Domain>(domains),
        IHaveJsonExtensionData
{
    public ListDomainsResponse()
        : this([]) { }

    [JsonExtensionData]
    public IDictionary<string, JElem> Properties { get; set; }

    public IDictionary<string, Domain> ToDictionary() => this.ToDictionary(x => x.Hostname, x => x);
}
