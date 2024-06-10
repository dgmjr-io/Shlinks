/*
 * TimeZoneInfoExpressionConverter.cs
 *     Created: 2024-05-05T23:48:13-04:00
 *    Modified: 2024-05-05T23:48:14-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Components.Converters;

using Newtonsoft.Json.Linq;

using Shlinks.Components.Expressions;

using Expression = AdaptiveExpressions.Expression;

public class TimeZoneExpressionConverter
    : Dgmjr.BotFramework.Converters.ExpressionConverter<TimeZoneExpression, TimeZoneInfo> { }
