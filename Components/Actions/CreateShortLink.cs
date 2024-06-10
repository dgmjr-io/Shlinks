namespace Shlinks.Components.Actions;

using System.Threading;
using System.Threading.Tasks;

using AdaptiveExpressions.Properties;

using Microsoft.Bot.Builder.Dialogs;

using Newtonsoft.Json;

using Shlinks.Services;

using Telegram.Bot.Components;

using Constants = Shlinks.Constants;

[CustomAction(ConstKind)]
[method: JsonConstructor]
public class CreateShortLink(
    ILoggerFactory loggerFactory,
    IServiceProvider services,
    [CallerFilePath] string sourceFilePath = "",
    [CallerLineNumber] int sourceLineNumber = 0
    ) : ShlinksAction<CreateShortLink>(ConstKind, services, loggerFactory, sourceFilePath, sourceLineNumber)
{
    public const string ConstKind = $"{Constants.Namespace}.{nameof(CreateShortLink)}";

    [JsonProperty("url")]
    public StrExp Url { get; set; }

    [JsonProperty("domain")]
    public StrExp Domain { get; set; }

    [JsonProperty("slug")]
    public StrExp Slug { get; set; }

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
        var url = Url.GetValue(outerDc.State);
        var domain = Domain.GetValue(outerDc.State);
        var slug = Slug.GetValue(outerDc.State);
        var telegramUsername = outerDc.State.GetValue<string>(UserData.UsernamePath);
        // telegramUsername.ThrowIfNullOrEmpty();
        var tags = Tags.GetValue(outerDc.State).Concat([telegramUsername]).ToArray();

        var service = GetShlinksService(outerDc);
        service.ThrowIfNull();
        var link = await service.CreateLinkAsync(new(url), domain, slug, tags);

        var resultProperty = ResultProperty.GetValue(outerDc.State);
        outerDc.State.SetValue(resultProperty, link);

        return await outerDc.EndDialogAsync(link, cancellationToken);
    }
}
