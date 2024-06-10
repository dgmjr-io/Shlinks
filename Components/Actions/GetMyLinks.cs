/*
 * GetMyLinks.cs
 *     Created: 2024-04-27T11:33:46-04:00
 *    Modified: 2024-04-27T11:33:47-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Components.Actions;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Dialogs.Adaptive.Conditions;

using Shlinks;
using Shlinks.Services;

using Telegram.Bot.Components;

using Constants = Constants;

[CustomAction(ConstKind)]
public class GetMyLinks(
    ILoggerFactory loggerFactory,
    IServiceProvider services,
    [CallerFilePath] string sourceFilePath = "",
    [CallerLineNumber] int sourceLineNumber = 0
) : ShlinksAction<GetMyLinks>(ConstKind, services, loggerFactory, sourceFilePath, sourceLineNumber)
{
    public const string ConstKind = $"{Constants.Namespace}.{nameof(GetMyLinks)}";

    [JsonProperty("domain")]
    [JProp("domain")]
    public StrExp Domain { get; set; } = new();

    [JsonProperty("fullResponse")]
    [JProp("fullResponse")]
    public BoolExpression IncludeFullResponse { get; set; } = new();

    protected virtual bool IsLinkAdmin(DialogContext dc) =>
        Constants.DefaultLinkAdministratorsExpression
            .GetValue(dc.State)
            .Contains(dc.State.GetStringValue(UserData.UsernamePath));

    public override async Task<DialogTurnResult> BeginDialogAsync(
        DialogContext outerDc,
        object? options = default,
        CancellationToken cancellationToken = default
    )
    {
        var resultProperty = await ResultProperty.GetValueAsync(outerDc, cancellationToken);
        var service = GetShlinksService(outerDc);
        service.ThrowIfNull();

        var domain =
            Domain.GetValue(outerDc) ?? Constants.DefaultDomainExpression.GetValue(outerDc.State);

        if (IncludeFullResponse.GetValue(outerDc))
        {
            var links = await service.GetMyLinksAsync(domain, null);
            links.Links = links.Links
                .Where(
                    link =>
                        IsLinkAdmin(outerDc)
                        || link.Tags.Contains(outerDc.State.GetStringValue(UserData.UsernamePath))
                )
                .ToList();
            outerDc.State.SetValue(ResultProperty.GetValue(outerDc.State), links);
            return await outerDc.EndDialogAsync(resultProperty, cancellationToken);
        }
        else
        {
            var links = await service.GetMyLinksAsync(domain, null);
            links.Links = new(
                links.Links
                    .Where(
                        link =>
                            IsLinkAdmin(outerDc)
                            || link.Tags.Contains(
                                outerDc.State.GetStringValue(UserData.UsernamePath)
                            )
                    )
                    .ToList()
            );
            outerDc.State.SetValue(ResultProperty.GetValue(outerDc.State), links);
            return await outerDc.EndDialogAsync(resultProperty, cancellationToken);
        }
    }
}
