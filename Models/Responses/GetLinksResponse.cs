/*
 * GetLinksResponse.cs
 *     Created: 2024-04-27T18:34:00-04:00
 *    Modified: 2024-04-27T18:34:00-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Responses;

using Microsoft.Bot.Builder.Dialogs.Adaptive.Input;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;

using Shlinks.Abstractions;
using Shlinks.Models.Abstractions;

public class GetLinksResponse() : IHaveJsonExtensionData, IShlinksApiResponse
{
    public List<Link> Links { get; set; } = [];

    public string NextPageToken { get; set; }
    public int Count { get; set; }

    [JsonExtensionData]
    public IDictionary<string, JElem> Properties { get; set; }


//     public ChoiceSet AsChoices => new (
//             Links.OrderBy(x => x.ShortUrl).Select(
//                 x =>
//                     new Choice(x.ShortUrl.ToString())
//                     {
//                         Value = x.ShortUrl.ToString(),
//                         Synonyms = [x.ShortUrl.ToString(), x.Slug, x.Url.ToString()],
//                         Action = new ()
//                         {
//                             Type = ActionTypes.ImBack,
//                             Title = x.ShortUrl.ToString(),
//                             Value = x.ShortUrl.ToString()
//                         }
//                     }
//             )
//         );

// #pragma warning disable S2365
//     public CardAction[] AsCardActions =>
//             Links.Select(
//                 x =>
//                         new CardAction()
//                         {
//                             Type = ActionTypes.ImBack,
//                             Title = x.ShortUrl.ToString(),
//                             DisplayText = x.ShortUrl.ToString(),
//                             Value = x.Id
//                         }).ToArray();
// #pragma warning restore S2365
}

// public class GetLinksBasicResponse(List<BasicLink> links) : List<BasicLink>(links)
// {
//     public GetLinksBasicResponse()
//         : this([]) { }

//     public static implicit operator GetLinksBasicResponse(GetLinksResponse links)
//         => new (links.Links.Select(link => new BasicLink
//         {
//             ShortUrl = link.ShortUrl,
//             LongUrl = link.Url,
//             Tags = link.Tags,
//             Slug = link.Slug,
//             Id = link.Id,
//             DomainId = link.DomainId,
//         }).ToList());


// }
