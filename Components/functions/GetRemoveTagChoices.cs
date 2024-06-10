namespace Shlinks.Bot.Functions;

using AdaptiveExpressions;
using AdaptiveExpressions.Memory;

[CustomFunction(ConstKind)]
public class GetRemoveTagChoices() : CustomFunction<SuggestedActions, string[]>(ConstKind, Evaluate)
{
    public const string ConstKind = "Shlinks." + nameof(GetRemoveTagChoices);
    private const string TagsProperty = "conversation.link.tags";

    private static SuggestedActions Evaluate(string[] tags)
    {
        var choices = new SuggestedActions(
            actions: tags.Select(tag => new CardAction { Title = tag, Value = tag }).ToArray()
        );
        return choices;
    }
}
