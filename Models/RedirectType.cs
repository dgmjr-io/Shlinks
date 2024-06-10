using System.Runtime.Serialization;

namespace Shlinks.Models;

[Obsolete("This enum isn't supported by the shlinks.io API.", true)]
public enum RedirectType
{
    [EnumMember(Value = "301")]
    Permanent = 301,

    [EnumMember(Value = "302")]
    Temporary = 302,

    [EnumMember(Value = "303")]
    SeeOther = 303,

    [EnumMember(Value = "307")]
    TemporaryRedirect = 307,

    [EnumMember(Value = "308")]
    PermanentRedirect = 308
}
