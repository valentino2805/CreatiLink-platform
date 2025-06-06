namespace CreatiLinkPlatform.API.IAM.Interfaces.REST.Resources;

/// <summary>
/// SignInResource 
/// </summary>
/// <param name="UserName">
/// The username of the user.
/// </param>
/// <param name="Password">
/// The password of the user.
/// </param>
public record SignInResource(string Email, string Password);
