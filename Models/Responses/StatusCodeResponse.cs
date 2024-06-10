/*
 * StatusCodeResponse.cs
 *     Created: 2024-06-11T06:28:07-04:00
 *    Modified: 2024-06-11T06:28:07-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Responses;

using Shlinks.Abstractions;

public class StatusCodeResponse : IShlinksApiResponse
{
    public System.Net.HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public bool IsSuccess =>
        StatusCode >= System.Net.HttpStatusCode.OK
        && StatusCode < System.Net.HttpStatusCode.Ambiguous;
}
