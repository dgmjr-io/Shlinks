namespace Shlinks.Models;

using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

public class Link : LinkBase
{
    [JProp("meta")]
    [JsonProperty("meta")]
    public Meta Meta { get; set; }

    [JProp("visitsSummary")]
    [JsonProperty("visitsSummary")]
    public VisitsSummary Visits { get; set; }
}
