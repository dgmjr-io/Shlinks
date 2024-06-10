/*
 * SelectLink.cs
 *     Created: 2024-05-15T19:33:43-04:00
 *    Modified: 2024-05-15T19:33:43-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Components.Actions;

using System.Collections.Generic;

using Microsoft.Bot.Builder.Dialogs.Adaptive.Conditions;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Input;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Templates;
using Microsoft.Bot.Builder.Dialogs.Choices;

using Shlinks.Extensions;
using Shlinks.Models;
using Shlinks.Responses;

using Telegram.Bot.Components;

using Activity = Microsoft.Bot.Schema.Activity;
using Attachment = Microsoft.Bot.Schema.Attachment;
using Constants = Constants;

[CustomAction(ConstKind)]
public class SelectLink(IServiceProvider services, ILoggerFactory loggerFactory)
    : ShlinksInputAction<SelectLink>(ConstKind, services, loggerFactory)
{
    public const string Turn_SelectedLink = "turn.selectedLink";
    public const string ConstKind = $"{Constants.Namespace}.{nameof(SelectLink)}";

    [JProp("domain")]
    [JsonProperty("domain")]
    public StrExp Domain { get; set; } = new();

    [JProp("property")]
    [JsonProperty("property")]
    public override StrExp ResultProperty { get; set; } = Turn_SelectedLink;

    private const string Cancel = nameof(Cancel);
    private static readonly CardAction CancelAction =
        new()
        {
            Type = ActionTypes.PostBack,
            Title = Cancel,
            DisplayText = Cancel,
            Text = Cancel,
            Value = Cancel
        };

    protected override async Task<InputState> OnRecognizeInputAsync(
        DialogContext dc,
        CancellationToken cancellationToken
    )
    {
        ResultProperty.SetValue(ResultProperty.GetValue(dc.State));
        var userInput = dc.Context.Activity.Text;
        if (userInput == Cancel)
        {
            return InputState.Valid;
        }
        var links = await GetLinks(dc);
        var selectedLink = links.FirstOrDefault(link => link.ShortUrl.ToString() == userInput);
        if (selectedLink != null)
        {
            dc.State.SetValue(ResultProperty.GetValue(dc.State), selectedLink);
            return InputState.Valid;
        }
        else
        {
            dc.State.SetValue(ResultProperty.GetValue(dc.State), null);
        }
        return InputState.Invalid;
    }

    protected override async Task<IActivity> OnRenderPromptAsync(
        DialogContext dc,
        InputState state,
        CancellationToken cancellationToken = default
    )
    {
        var prompt = await base.OnRenderPromptAsync(dc, state, cancellationToken);
        var messageActivity = prompt.AsMessageActivity();

        var linkActions = await GetCardActions(dc);
        if (linkActions.Count == 1)
        {
            messageActivity.Text =
                "You don't have any links yet. Would you like to create one?  Send /newlink to get started.";
            return messageActivity;
        }

        // var hc = new HeroCard { Buttons = linkActions };

        // messageActivity.Attachments = [hc.ToAttachment()];

        var suggestedActions = MessageFactory.SuggestedActions(
            linkActions,
            messageActivity.Text,
            null,
            InputHints.ExpectingInput
        );

        messageActivity.SuggestedActions = suggestedActions.SuggestedActions;
        return messageActivity;
    }

    protected virtual async Task<IEnumerable<Link>> GetLinks(DialogContext dc)
    {
        var service = GetShlinksService(dc);
        service.ThrowIfNull();

        var domain =
            Domain.GetValue(dc.State) ?? Constants.DefaultDomainExpression.GetValue(dc.State);

        // await dc.Context.SendActivityAsync(
        //     MessageFactory.Text($"Retrieving links for {domain}...")
        // );

        var links = (await service.GetMyLinksAsync(domain, null)).Links.Where(
            link =>
                IsLinkAdmin(dc)
                || link.UserCanEditOrDelete(dc.State.GetStringValue(UserData.UsernamePath))
        );
        return links;
    }

    protected virtual async Task<IList<CardAction>> GetCardActions(DialogContext dc)
    {
        var links = await GetLinks(dc);
        var linkActions = links.AsCardActions().ToList();
        linkActions.Insert(0, CancelAction);
        return linkActions;
    }

    protected virtual bool IsLinkAdmin(DialogContext dc) =>
        Constants.DefaultLinkAdministratorsExpression
            .GetValue(dc.State)
            .Contains(dc.State.GetStringValue(UserData.UsernamePath));
}
