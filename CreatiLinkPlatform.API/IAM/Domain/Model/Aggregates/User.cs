namespace CreatiLinkPlatform.API.IAM.Domain.Model.Aggregates;


public class Users(string email, string password, string role)
{
    public int Id { get; }

    public string Email { get; private set; } = email;
    
    public string Password { get; private set; } = password;
    
    public string Role { get; private set; } = role;
    
    public int? ProfileId { get; private set; }
    
    public Users AssignProfileIdFromUserId()
    {
        ProfileId = Id;
        return this;
    }
    
    public Users UpdateEmail(string email)
    {
        Email = email;
        return this;
    }
    
    public Users UpdatePassword(string password)
    {
        Password = password;
        return this;
    }
    
    public Users UpdateRole(string role)
    {
        Role = role;
        return this;
    }
    

}