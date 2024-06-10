namespace Shlinks;

using System.Collections;

using Microsoft.Extensions.Configuration;

using Shlinks.Models;

public class Settings
{
    public Settings() { }

    public Settings(IConfigurationSection? configuration)
    {
        configuration?.Bind(this);
    }
    public Settings(IConfiguration? configuration)
    {
        configuration?.Bind(ConfigurationKey, this);
    }

    public const string ConfigurationKey = $"{nameof(Shlinks)}";
    public const string DefaultBaseUrl = "https://iamtheantitwinksshortlinks.azurewebsites.net";
    public const string DefaultStatsBaseUrl = DefaultBaseUrl;

    // public virtual duration DefaultTimeToLive { get; set; } = duration.FromDays(30);
    public virtual string ApiKey { get; set; }
    public virtual string DefaultDomain { get; set; }
    public virtual IDictionary<string, DomainSettings> Domains { get; set; } = new Dictionary<string, DomainSettings>();
    public virtual bool SupportsMultipleDomains => Domains.Count > 1;
    public virtual string[] LinkAdministrators { get; set; } = [];
    public virtual string[] LinkCreators { get; set; } = [];
    public virtual Uri BaseUrl { get; set; } = new(DefaultBaseUrl);
    public virtual Uri Default404Redirect { get; set; } = new("https://google.com");
    public virtual string AdministratorUsername { get; set; } = "@dgmjr";
    public virtual string UnauthorizedMessage { get; set; } = "Sorry, you're not authorized to do that.  If you think this is a mistake or you'd like to request access, contact the bot's administrator.";
}
