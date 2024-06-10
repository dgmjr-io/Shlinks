/*
 * ListDomains.cs
 *     Created: 2024-05-01T02:57:57-04:00
 *    Modified: 2024-05-01T02:57:58-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Components.Actions;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Dialogs;

[CustomAction(ConstKind)]
public class ListDomains(
    ILoggerFactory loggerFactory,
    IServiceProvider services,
    [CallerFilePath] string sourceFilePath = "",
    [CallerLineNumber] int sourceLineNumber = 0
) : ShlinksAction<ListDomains>(ConstKind, services, loggerFactory, sourceFilePath, sourceLineNumber)
{
    public const string ConstKind = $"{Constants.Namespace}.{nameof(ListDomains)}";

    public override async Task<DialogTurnResult> BeginDialogAsync(
        DialogContext outerDc,
        object? options = default,
        CancellationToken cancellationToken = default
    )
    {
        var domains = await GetShlinksService(outerDc).GetDomainsAsync();
        outerDc.State.SetValue(ResultProperty.GetValue(outerDc.State), domains);

        return await outerDc.EndDialogAsync(result: domains, cancellationToken);
    }
}
