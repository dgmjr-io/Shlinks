/*
 * ShlinksBotComponent.cs
 *     Created: 2024-49-19T22:49:01-04:00
 *    Modified: 2024-49-19T22:49:01-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Components;

using Dgmjr.BotFramework;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Shlinks.Services;
using Shlinks.Bot.Functions;
using Shlinks.Components.Actions;

using Expression = AdaptiveExpressions.Expression;
using JsonConverterFactory = Microsoft.Bot.Builder.Dialogs.Declarative.Converters.JsonConverterFactory;

public class ShlinksBotComponent : CustomBotComponent
{
    public override void ConfigureServices(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        base.ConfigureServices(services, configuration);

        services.AddCustomJsonConverter(typeof(ArrayExpressionConverter<BoolExpression>));
        services.RegisterCustomFunction<LinksToChoices>(LinksToChoices.ConstKind);
    }
}
