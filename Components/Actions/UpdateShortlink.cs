/*
 * UpdateShortLink.cs
 *     Created: 2024-06-01T02:59:51-04:00
 *    Modified: 2024-06-01T02:59:51-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Components.Actions;

using System.Threading;
using System.Threading.Tasks;

using AdaptiveExpressions.Properties;

using Microsoft.Bot.Builder.Dialogs;

using Newtonsoft.Json;

using OpenTelemetry.Trace;

using Shlinks.Services;

using Telegram.Bot.Components;

using Constants = Shlinks.Constants;

using Link = Shlinks.Models.Link;

[CustomAction(ConstKind)]
[method: JsonConstructor]
public class UpdateShortlink(
    ILoggerFactory loggerFactory,
    IServiceProvider services,
    [CallerFilePath] string sourceFilePath = "",
    [CallerLineNumber] int sourceLineNumber = 0
    ) : ShlinksAction<UpdateShortlink>(ConstKind, services, loggerFactory, sourceFilePath, sourceLineNumber)
{
    public const string ConstKind = $"{Constants.Namespace}.{nameof(UpdateShortlink)}";

    [JsonProperty("url")]
    public StrExp Url { get; set; }

    [JsonProperty("domain")]
    public StrExp Domain { get; set; }

    [JsonProperty("slug")]
    public StrExp Slug { get; set; }

    [JsonProperty("linkId")]
    public StrExp LinkId { get; set; }

    [JsonProperty("tags")]
    public ArrayExpression<string> Tags { get; set; }

    [JsonProperty("validation")]
    public ArrayExpression<BoolExpression> Validation { get; set; }

    public override async Task<DialogTurnResult> BeginDialogAsync(
        DialogContext outerDc,
        object? options = default,
        CancellationToken cancellationToken = default
    )
    {
        var linkId = LinkId.GetValue(outerDc.State);
        var url = new Uri(Url.GetValue(outerDc.State));
        var domain = Domain.GetValue(outerDc.State);
        var slug = Slug.GetValue(outerDc.State);
        var telegramUsername = outerDc.State.GetValue<string>(UserData.UsernamePath);

        // telegramUsername.ThrowIfNullOrEmpty();
        var tags = Tags.GetValue(outerDc.State).Concat([telegramUsername]).ToArray();

        var service = GetShlinksService(outerDc);
        service.ThrowIfNull();
        var domains = await service.GetDomainsAsync();
        var domainId = domains.Find(d => d.Hostname == domain)?.Id  ?? throw new KeyNotFoundException($"Domain '{domain}' not found");

        var link = await service.UpdateLinkAsync(new Link { Id = linkId, DomainId = domainId, Url = url });

        var resultProperty = ResultProperty.GetValue(outerDc.State);
        outerDc.State.SetValue(resultProperty, link);

        return await outerDc.EndDialogAsync(link, cancellationToken);
    }
}
