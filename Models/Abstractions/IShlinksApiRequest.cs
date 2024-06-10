/*
 * IShlinksApiRequest.cs
 *     Created: 2024-06-10T21:49:33-04:00
 *    Modified: 2024-06-10T21:49:33-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

using MediatR;

namespace Shlinks.Abstractions;

public interface IShlinksApiRequest<TResponse> : IRequest<TResponse>
    where TResponse : IShlinksApiResponse
{
    ApiVersion ApiVersion { get; set; }
}
