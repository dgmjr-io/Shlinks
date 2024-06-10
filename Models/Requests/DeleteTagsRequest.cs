/*
 * DeleteTagRequest.cs
 *     Created: 2024-06-11T10:23:28-04:00
 *    Modified: 2024-06-11T10:23:29-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks;

public class DeleteTagsRequest : DeleteRequest
{
    public virtual string[] Tags { get; set; } = [];
}
