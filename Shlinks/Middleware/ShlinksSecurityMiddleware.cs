using AdaptiveCards.Rendering;

namespace Shlinks.Security.Middleware;

public class ShlinksSecurityMiddleware(
    IOptions<Settings> options,
    Telegram.Bot.Components.UserDataAccessor userData
) : IBotMiddleware
{
    private readonly Settings _settings = options.Value;

    private async Task<string?> GetCurrentUserTelegramUsername(ITurnContext turnContext)
    {
        var user = await userData.GetAsync(turnContext);
        return user.Username;
    }

    public virtual async Task OnTurnAsync(
        ITurnContext turnContext,
        NextDelegate next,
        CancellationToken cancellationToken = default
    )
    {
        if (
            turnContext.TurnState.TryGetValue(
                typeof(RecognizerResult).FullName,
                out var recognizerResult
            )
        )
        {
            var result = recognizerResult as RecognizerResult;
            if (LuisRecognizer.TopIntent(result) == ShlinksConstants.NewLinkIntent)
            {
                if (
                    _settings.LinkCreators.Contains(
                        await GetCurrentUserTelegramUsername(turnContext)
                    )
                )
                {
                    await next(cancellationToken);
                }
                else
                {
                    await turnContext.SendActivityAsync(
                        MessageFactory.Text(_settings.UnauthorizedMessage),
                        cancellationToken
                    );
                }
            }
        }
        else
        {
            await next(cancellationToken);
        }
    }
}
