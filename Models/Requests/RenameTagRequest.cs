/*
 * RenameTagRequest.cs
 *     Created: 2024-06-11T06:52:05-04:00
 *    Modified: 2024-06-11T06:52:05-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Requests;

using Shlinks.Abstractions;
using Shlinks.Responses;

public class RenameTagRequest : IShlinksApiRequest<StatusCodeResponse>
{
    public ApiVersion ApiVersion { get; set; } = ApiVersion.Latest;

    public string OldName { get; set; } = default!;
    public string NewName { get; set; } = default!;
}
