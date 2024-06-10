/*
 * GetTags.cs
 *     Created: 2024-46-21T01:46:21-04:00
 *    Modified: 2024-46-21T01:46:21-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */


namespace Shlinks.Components.Actions;

using AdaptiveExpressions.Properties;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Activity = Microsoft.Bot.Schema.Activity;

[CustomAction(MainDialogId)]
public partial class PromptForTags : BotCustomComponentAction<PromptForTags>
{
    private const string LinkPath = "conversation.link";
    private const string TagsPath = LinkPath + ".tags";
    private const string DomainPath = LinkPath + ".domain";
    private const string DefaultTagsPath = "settings.Shlinks.defaultTags[{0}]";
    private const string Done = "/done";
    private const string Cancel = "/cancel";
    private const string Remove = "/remove";
    public const string MainDialogId = nameof(PromptForTags);
    public const string RemoveDialogId = nameof(PromptForTags) + Remove;

    private const string RemoveCommandRegexString = "/remove_(?<tag>[a-zA-Z0-9]+)";
    [GeneratedRegex(RemoveCommandRegexString)]
    private partial Regex RemoveCommandRegex();

    private const string NonAtoZ0to9CharsRegexString = @"[^a-zA-Z0-9]";
    [GeneratedRegex(NonAtoZ0to9CharsRegexString)]
    private partial Regex NonAtoZ0to9CharsRegex();

    private string MakeRemoveCommand(string tag)
    {
        return "/remove_" + NonAtoZ0to9CharsRegex().Replace(tag, "");
    }

    private string MakeRemoveCommandMenuItem(string tag)
    {
        return MakeRemoveCommand(tag) + " - Remove \"" + tag + "\"";
    }

    [JsonProperty("resultProperty")]
    [JProp("resultProperty")]
    public override StrExp ResultProperty { get; set; } = TagsPath;

    [JsonProperty("domainProperty")]
    [JProp("domainProperty")]
    public virtual StrExp DomainProperty { get; set; } = DomainPath;

    private async Task<string[]> GetTags(DialogContext dc) =>
        await Task.FromResult(dc.State.GetValue<string[]>(TagsPath)
        ?? dc.State.GetValue<string[]>(
            Format(DefaultTagsPath, dc.State.GetValue<string>(DomainProperty.GetValue(dc.State)))
        )
        ?? Empty<string>());

    private async Task SetTags(DialogContext dc, string[] tags) =>
        await Task.Run(() => dc.State.SetValue(ResultProperty.GetValue(dc.State), tags));

    private async Task RemoveTag(DialogContext dc, string tag) =>
        await SetTags(dc, (await GetTags(dc)).Except([tag]).OrderBy(t => t).Distinct().ToArray());

    private async Task AddTag(DialogContext dc, string tag) =>
        await SetTags(dc, (await GetTags(dc)).Append(tag).OrderBy(t => t).Distinct().ToArray());

    public PromptForTags(
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
        : base(MainDialogId, sourceFilePath, sourceLineNumber)
    {
        var waterfallSteps = new WaterfallStep[] { PromptForInputAsync, HandleInputAsync };
        var removeWaterfallSteps = new WaterfallStep[] { PromptForRemoveAsync, HandleRemoveAsync };

        AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
        AddDialog(new WaterfallDialog(MainDialogId, waterfallSteps));
        AddDialog(new WaterfallDialog(RemoveDialogId, removeWaterfallSteps));
        AddDialog(new TextPrompt(nameof(TextPrompt)));
    }

    public async Task<DialogTurnResult> PromptForInputAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken
    )
    {
        // Prompt for input
        return await stepContext.PromptAsync(
            nameof(TextPrompt),
            new PromptOptions
            {
                Prompt = MessageFactory.Text(
                    $$$"""
                    Enter a "tag" for this link.


                    Type /done when you're finished.

                    Send /remove to remove a tag.


                    Tags you've alread entered are listed below:


                    {{{Join("\n\n", (await GetTags(stepContext)).Select(t => $"- {t}"))}}}
                    """
                )
            },
            cancellationToken
        );
    }

    public async Task<DialogTurnResult> PromptForRemoveAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken
    )
    {
        var options = new PromptOptions
        {
            Prompt = MessageFactory.Text(
                    $$$"""
                    Which tag would you like to remove?  Select from one of the options below or send \"/cancel\" to cancel.
                    """
                ),
            Choices = ChoiceFactory.ToChoices((await GetTags(stepContext)).ToList()),
            Style = ListStyle.SuggestedAction
        };
        // Prompt for input
        return await stepContext.PromptAsync(
            nameof(ChoicePrompt),
            options,
            cancellationToken
        );
    }

    public async Task<DialogTurnResult> HandleRemoveAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken
    )
    {
        var userInput = (string)stepContext.Result;
        var allTags = await GetTags(stepContext);
        // Check if the user didn't enter the cancellation command
        if (!userInput.Equals(Cancel, OrdinalIgnoreCase))
        {
            var tag2Remove = Find(allTags, tag => tag == userInput);
            if(!IsNullOrEmpty(tag2Remove))
            {
                await RemoveTag(stepContext, tag2Remove);
            }
        }
        return await stepContext.ReplaceDialogAsync(MainDialogId, null, cancellationToken);
    }

    public async Task<DialogTurnResult> HandleInputAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken
    )
    {
        var userInput = (string)stepContext.Result;

        // Check if the user entered the sentinel value
        if (userInput.Equals(Done, OrdinalIgnoreCase))
        {
            // User is done, proceed to the next step or end the dialog
            return await stepContext.EndDialogAsync(await GetTags(stepContext), cancellationToken);
        }
        else if (userInput.Equals(Remove, OrdinalIgnoreCase))
        {
            return await stepContext.ReplaceDialogAsync(RemoveDialogId, null, cancellationToken);
        }

        // Loop back to prompt for more input
        return await stepContext.ReplaceDialogAsync(MainDialogId, null, cancellationToken);
    }
}
