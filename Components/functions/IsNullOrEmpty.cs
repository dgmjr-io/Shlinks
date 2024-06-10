// /*
//  * IsNullOrEmpty.cs
//  *
//  *   Created: 2024-44-18T18:44:06-04:00
//  *   Modified: 2024-44-18T18:44:07-04:00
//  *
//  *   Author: David G. Moore, Jr. <david@dgmjr.io>
//  *
//  *   Copyright © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
//  *      License: MIT (https://opensource.org/licenses/MIT)
//  */

// namespace Shlinks.Bot.Functions;

// using AdaptiveExpressions;
// using AdaptiveExpressions.Memory;

// using Microsoft.Bot.Builder.Dialogs.Adaptive.Functions;
// using Microsoft.Bot.Builder.Dialogs.Memory;

// [CustomFunction($"{strings}.{nameof(IsNullOrEmpty)}")]
// public class IsNullOrEmpty()
//     : CustomFunction<IsNullOrEmpty, bool>(
//         name: $"{strings}.{nameof(IsNullOrEmpty)}",
//         returnType: ReturnType.Boolean
//     )
// {
//     private const string strings = "str";

//     public const string Kind = strings + "." + nameof(IsNullOrEmpty);

//     protected override (bool, string?) Evaluate(
//         Expression expression,
//         IMemory state,
//         Options options
//     )
//     {
//         var (value, error) = expression.Children[0].TryEvaluate<string>(state, options);

//         if (error != null)
//         {
//             return (true, error);
//         }

//         return (string.IsNullOrEmpty(value), null);
//     }
// }
