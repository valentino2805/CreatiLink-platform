namespace CreatiLinkPlatform.API.IAM.Infrastructure.Tokens.JWT.Configuration;

/// <summary>
/// Token settings. 
/// </summary>
public class TokenSettings
{
    public string Secret { get; set; }
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
}