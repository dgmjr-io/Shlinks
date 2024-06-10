/*
 * CreateDomainRequest.cs
 *
 *   Created: 2024-07-17T11:07:31-04:00
 *   Modified: 2024-07-17T11:07:31-04:00
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Shlinks.Requests;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Shlinks.Models;
using Shlinks.Models.Abstractions;

[Obsolete("This operation is obsolete.  Domains can't be created via the API.", true)]
public record class CreateDomainRequest : Domain { }
