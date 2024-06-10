/*
 * LinkExtensions.cs
 *     Created: 2024-05-16T18:27:20-04:00
 *    Modified: 2024-05-16T18:27:21-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Extensions;

using Shlinks.Models;

public static class LinkExtensions
{
    public static CardAction AsCardAction(this Link link) =>
        new()
        {
            Type = ActionTypes.ImBack,
            Title = link.ShortUrl.ToString(),
            DisplayText = link.ShortUrl.ToString(),
            Text = link.ShortUrl.ToString(),
            Value = link.Id
        };

    public static bool UserCanEditOrDelete(this Link link, string username) =>
        link.Tags.Contains(username);
}
