/*
 * DeleteLinkRequest.cs
 *     Created: 2024-06-11T06:26:39-04:00
 *    Modified: 2024-06-11T06:26:39-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Requests;

using Shlinks.Abstractions;
using Shlinks.Responses;

public class DeleteLinkRequest : DeleteRequest
{
    public virtual string Slug { get; set; }
    public virtual string Domain { get; set; }
}
