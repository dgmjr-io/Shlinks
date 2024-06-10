/*
 * TimeZoneInfoExpressionConverter.cs
 *     Created: 2024-05-05T23:48:13-04:00
 *    Modified: 2024-05-05T23:48:14-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Components.Expressions;

using Newtonsoft.Json.Linq;

using Expression = AdaptiveExpressions.Expression;

[CustomExpression(Kind)]
public class TimeZoneExpression : ExpressionProperty<TimeZoneInfo>
{
    #pragma warning disable CS0618
    public const string Kind = $"{Constants.Namespace}.{nameof(TimeZone)}";
    #pragma warning restore

    public TimeZoneExpression(byte[] value)
        : this(value as object) { }

    public TimeZoneExpression(string expressionOrString)
        : base(expressionOrString) { }

    public TimeZoneExpression(Expression expression)
        : base(expression) { }

    public TimeZoneExpression(Func<object, object> expression)
        : base(Expression.Lambda(expression)) { }

    public TimeZoneExpression(JToken expressionOrValue)
        : base(expressionOrValue) { }

    public TimeZoneExpression(TimeZoneInfo value)
        : base(value.Id) { }

    public TimeZoneExpression(object value)
        : base(value) { }

    public TimeZoneExpression()
        : base(TimeZoneInfo.Local) { }

    public static implicit operator TimeZoneExpression(TimeZoneInfo value) => new(value);

    public static implicit operator TimeZoneExpression(string expressionOrString) =>
        new(expressionOrString);

    public static implicit operator TimeZoneExpression(Expression expression) => new(expression);

    public static implicit operator TimeZoneExpression(byte[] value) => new(value);

    public override void SetValue(object? value)
    {
        if (value is Expression or long or int or ChatId)
        {
            base.SetValue(value.ToString());
            return;
        }
        value = value is JValue value1 ? value1.Value : value;
        var text = value?.ToString();
        if (text == null)
        {
            return;
        }
        if (text.StartsWith('='))
        {
            ExpressionText = text;
            return;
        }
        if (text.StartsWith("\\=", Ordinal))
        {
            text = text.TrimStart('\\');
        }

        ExpressionText = "=`" + text.Replace("`", "\\`") + "`";
    }
}
