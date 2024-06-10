/*
 * QueryRequest.cs
 *     Created: 2024-06-11T10:26:17-04:00
 *    Modified: 2024-06-11T10:26:17-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Requests;

using Shlinks.Abstractions;

public abstract class QueryRequest<TResult> : IShlinksApiRequest<TResult>
    where TResult : class, IShlinksApiResponse
{
    public ApiVersion ApiVersion { get; set; } = ApiVersion.Latest;
}
