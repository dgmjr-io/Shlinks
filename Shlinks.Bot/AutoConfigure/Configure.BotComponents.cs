namespace Shlinks.Bot.Configure;

using Telegram.Bot.Components;
using Shlinks.Components.Actions;

public class BotComponents : ConfiguratorBase<BotComponents>
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        Logger.ConfiguringService(
            $"{nameof(Configure)}.{nameof(BotComponents)}",
            Environment?.EnvironmentName
        );
        // services.AddSingleton<TelegramBotComponent>();

        // Logger.RegisteringCustomFunction(Functions.Join.ConstKind, typeof(Functions.Join));
        // AdaptiveExpressions.Expression.Functions.Add(
        //     Functions.Join.ConstKind,
        //     new Functions.Join()
        // );

        // Logger.RegisteringCustomFunction(Functions.IsValidUrl.Kind, typeof(Functions.IsValidUrl));
        // AdaptiveExpressions.Expression.Functions.Add(
        //     Functions.IsValidUrl.Kind,
        //     new Functions.IsValidUrl()
        // );

        // Logger.RegisteringCustomFunction(
        //     Functions.IsNullOrEmpty.Kind,
        //     typeof(Functions.IsNullOrEmpty)
        // );
        // AdaptiveExpressions.Expression.Functions.Add(
        //     Functions.IsNullOrEmpty.Kind,
        //     new Functions.IsNullOrEmpty()
        // );

        // Logger.RegisteringCustomFunction(
        //     Functions.GetRemoveTagChoices.ConstKind,
        //     typeof(Functions.GetRemoveTagChoices)
        // );
        // AdaptiveExpressions.Expression.Functions.Add(
        //     Functions.GetRemoveTagChoices.ConstKind,
        //     new Functions.GetRemoveTagChoices()
        // );

        // services.AddSingleton<DeclarativeType>(
        //     new DeclarativeType<PromptForTags>(PromptForTags.MainDialogId)
        // );
        // services.AddSingleton<DeclarativeType>(
        //     new DeclarativeType<CreateShortLink>(CreateShortLink.ConstKind)
        // );
    }
}
