/*
 * DeleteRequest.cs
 *     Created: 2024-06-11T10:23:35-04:00
 *    Modified: 2024-06-11T10:23:36-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Requests;

using Shlinks.Abstractions;
using Shlinks.Responses;

public abstract class DeleteRequest : IShlinksApiRequest<StatusCodeResponse>
{
    public ApiVersion ApiVersion { get; set; } = ApiVersion.Latest;
}
