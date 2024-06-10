/*
 * LinksToChoices.cs
 *     Created: 2024-05-03T14:32:29-04:00
 *    Modified: 2024-05-03T14:32:30-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Bot.Functions;

using AdaptiveExpressions;

using Microsoft.Bot.Builder.Dialogs.Adaptive.Input;
using Microsoft.Bot.Builder.Dialogs.Choices;

using Shlinks.Responses;

[CustomFunction(ConstKind)]
public class LinksToChoices()
    : CustomFunction<ChoiceSet, GetLinksResponse>(ConstKind, EvaluateLinksToChoices)
{
    public const string ConstKind = "Shlinks." + nameof(LinksToChoices);

    public static ChoiceSet EvaluateLinksToChoices(GetLinksResponse response)
    {
        return new ChoiceSet(
            response.Links.Select(
                x =>
                    new Choice(x.ShortUrl.ToString())
                    {
                        Value = x.ShortUrl.ToString(),
                        Synonyms = [x.ShortUrl.ToString()],
                        Action = new ()
                        {
                            Type = ActionTypes.ImBack,
                            Title = x.ShortUrl.ToString(),
                            Value = x.Slug
                        }
                    }
            )
        );
    }
}
