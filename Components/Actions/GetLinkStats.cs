/*
 * GetLinkStats.cs
 *     Created: 2024-05-05T23:36:08-04:00
 *    Modified: 2024-05-05T23:36:19-04:00
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
using Shlinks.Components.Converters;
using Shlinks.Components.Expressions;
using Shlinks.Extensions;
using Shlinks.Requests;

[CustomAction(ConstKind)]
[method: JsonConstructor]
public class GetLinkStats(
    ILoggerFactory loggerFactory,
    IServiceProvider services,
    [CallerFilePath] string sourceFilePath = "",
    [CallerLineNumber] int sourceLineNumber = 0
)
    : ShlinksAction<GetLinkStats>(
        ConstKind,
        services,
        loggerFactory,
        sourceFilePath,
        sourceLineNumber
    )
{
    public const string ConstKind = $"{Constants.Namespace}.{nameof(GetLinkStats)}";

    [JsonProperty("linkId")]
    public StrExp LinkId { get; set; } = "=turn.recognized.entities.slug[0]";

    [JsonProperty("timeZone")]
    public TimeZoneExpression TimeZone { get; set; } = TimeZoneInfo.Local;

    [JsonProperty("period")]
    public EnumExpression<Period> Period { get; set; } = Requests.Period.AllTime;

    [JsonProperty("showStats")]
    public BoolExpression ShowStats { get; set; } = true;

    [JsonProperty("clicksChartInterval")]
    public EnumExpression<ClicksChartInterval> ClicksChartInterval { get; set; } =
        Requests.ClicksChartInterval.Hour;

    public override async Task<DialogTurnResult> BeginDialogAsync(
        DialogContext outerDc,
        object? options = default,
        CancellationToken cancellationToken = default
    )
    {
        var linkId = LinkId.GetValue(outerDc.State);
        var service = GetShlinksStatsService(outerDc);
        service.ThrowIfNull();
        var stats = await service.GetLinkStatsAsync(
            linkId,
            Period.GetValue(outerDc.State),
            TimeZone.GetValue(outerDc.State),
            ClicksChartInterval.GetValue(outerDc.State)
        );

        var resultProperty = ResultProperty.GetValue(outerDc.State);
        outerDc.State.SetValue(resultProperty, stats);

        if (ShowStats.GetValue(outerDc.State))
        {
            var message = stats.ToMessage();
            await outerDc.Context.SendActivityAsync(message, cancellationToken);
        }

        return await outerDc.EndDialogAsync(stats, cancellationToken);
    }
}
