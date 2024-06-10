namespace Shlinks.Models;

using System.Text.Json;
using System.Text.Json.Serialization;

[Obsolete("This enum isn't supported by the shlinks.io API.", true)]
[JsonConverter<JsonStringEnumConverter<SlugGenerationAlgorithm>>]
public enum SlugGenerationAlgorithm
{
    None,
    Random = None,
    Increment,
    Secure
}
