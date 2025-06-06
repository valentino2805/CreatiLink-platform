namespace CreatiLinkPlatform.API.IAM.Interfaces.ACL;


public interface IIamContextFacade
{

    Task<int> CreateUser(string email, string password, string role);

    // Buscar Id por email (antes username)
    Task<int> FetchUserIdByEmail(string email);

    // Buscar email por Id (antes username)
    Task<string> FetchEmailByUserId(int userId);
}
