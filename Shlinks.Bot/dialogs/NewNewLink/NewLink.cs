/*
 * NewNewLink.cs
 *     Created: 2024-03-19T15:03:53-04:00
 *    Modified: 2024-03-19T15:03:55-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks;
using Uri = System.Uri;

public class NewLinkDialog : ComponentDialog
{
    public NewLinkDialog()
        : base(nameof(NewLinkDialog))
    {
        AddDialog(new TextPrompt(nameof(TextPrompt)));
        AddDialog(
            new WaterfallDialog(
                nameof(WaterfallDialog),
                new WaterfallStep[]
                {
                    UrlStepAsync,
                    SlugStepAsync,
                    TagsStepAsync,
                    SummaryStepAsync,
                }
            )
        );

        InitialDialogId = nameof(WaterfallDialog);
    }

    private async Task<DialogTurnResult> UrlStepAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken
    )
    {
        var linkInfo = (LinkInfo)stepContext.Options ?? new LinkInfo();

        // Check if the user is navigating back to this step
        if (
            stepContext.Result is string input
            && (
                input.Equals("/slug", OrdinalIgnoreCase)
                || input.Equals("/tags", OrdinalIgnoreCase)
            )
        )
        {
            // Prompt for URL
            return await stepContext.PromptAsync(
                nameof(TextPrompt),
                new PromptOptions { Prompt = MessageFactory.Text("Please enter a URL:") },
                cancellationToken
            );
        }
        else if (stepContext.Result is string input2 && !string.IsNullOrEmpty(input2) && IsValidUrl(input2))
        {
            linkInfo.Url = input2;
            return await stepContext.NextAsync(linkInfo, cancellationToken);
        }
        else
        {
            await stepContext.Context.SendActivityAsync(
                MessageFactory.Text("Invalid URL. Please enter a valid URL."),
                cancellationToken
            );
            return await stepContext.ReplaceDialogAsync(
                InitialDialogId,
                linkInfo,
                cancellationToken
            );
        }
    }

    private async Task<DialogTurnResult> SlugStepAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken
    )
    {
        var linkInfo = (LinkInfo)stepContext.Options;

        // Prompt for Slug
        return await stepContext.PromptAsync(
            nameof(TextPrompt),
            new PromptOptions { Prompt = MessageFactory.Text("Please enter a slug:") },
            cancellationToken
        );
    }

    private async Task<DialogTurnResult> TagsStepAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken
    )
    {
        var linkInfo = (LinkInfo)stepContext.Options;

        // Prompt for Tags
        return await stepContext.PromptAsync(
            nameof(TextPrompt),
            new PromptOptions
            {
                Prompt = MessageFactory.Text("Please enter tags (comma separated):")
            },
            cancellationToken
        );
    }

    private async Task<DialogTurnResult> SummaryStepAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken
    )
    {
        var linkInfo = (LinkInfo)stepContext.Options;

        // Assuming the tags are the last input
        linkInfo.Tags = new List<string>(stepContext.Result.ToString().Split(','));

        // Display summary
        var message =
            $"URL: {linkInfo.Url}\nSlug: {linkInfo.Slug}\nTags: {string.Join(", ", linkInfo.Tags)}";
        await stepContext.Context.SendActivityAsync(
            MessageFactory.Text(message),
            cancellationToken
        );

        return await stepContext.EndDialogAsync(linkInfo, cancellationToken);
    }

    private bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, Absolute, out var uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    private class LinkInfo : DialogState
    {
        public string Url { get; set; }
        public string Slug { get; set; }
        public List<string> Tags { get; set; } = [];
    }
}
