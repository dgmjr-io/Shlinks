/*
 * CreateShortLink copy.cs
 *     Created: 2024-05-08T16:16:21-04:00
 *    Modified: 2024-05-08T16:16:21-04:00
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

using Shlinks.Services;

[CustomAction(ConstKind)]
[method: JsonConstructor]
public class DeleteShortLink(
    ILoggerFactory loggerFactory,
    IServiceProvider services,
    [CallerFilePath] string sourceFilePath = "",
    [CallerLineNumber] int sourceLineNumber = 0
)
    : ShlinksAction<DeleteShortLink>(
        ConstKind,
        services,
        loggerFactory,
        sourceFilePath,
        sourceLineNumber
    )
{
    public const string ConstKind = $"{Constants.Namespace}.{nameof(DeleteShortLink)}";

    [JsonProperty("linkId")]
    public StrExp LinkId { get; set; }

    public override async Task<DialogTurnResult> BeginDialogAsync(
        DialogContext outerDc,
        object? options = default,
        CancellationToken cancellationToken = default
    )
    {
        var linkId = LinkId.GetValue(outerDc.State);

        var service = GetShlinksService(outerDc);
        service.ThrowIfNull();
        await service.DeleteLinkAsync(linkId);
        return await outerDc.EndDialogAsync(null, cancellationToken);
    }
}
