/*
 * LinkCollectionExtensions.cs
 *     Created: 2024-05-16T18:29:14-04:00
 *    Modified: 2024-05-16T18:29:14-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

using Shlinks.Models;

namespace Shlinks.Extensions;

public static class LinkCollectionExtensions
{
    public static IEnumerable<CardAction> AsCardActions(this IEnumerable<Link> links) =>
        links.Select(LinkExtensions.AsCardAction);
}
