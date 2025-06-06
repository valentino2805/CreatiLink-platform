using CreatiLinkPlatform.API.IAM.Domain.Model.Aggregates;

namespace CreatiLinkPlatform.API.Profile.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
public class Profile
{
    public int Id { get; set; }
    public string Name { get; private set; }      
    public string Location { get; private set; }
    public string Bio { get; private set; }
    public string Image { get; private set; }
    public string Icon { get; private set; }
    public List<string> Experience { get; private set; }
    public SocialLinks Social { get; private set; }
    
    public int UserId { get; private set; }
    public Users? User { get; set; }
    
    private Profile() 
    {
        Experience = new List<string>();
        Social = new SocialLinks();
    }

    public Profile(int id, int userId ,string name, string location, string bio, string image, string icon, List<string> experience, SocialLinks social)
    {
        Id = id;
        UserId = userId;
        Name = name;
        Location = location;
        Bio = bio;
        Image = image;
        Icon = icon;
        Experience = experience;
        Social = social;
    }

    public void UpdateProfile(string name, string location, string bio, string image, string icon, List<string> experience, SocialLinks social)
    {
        Name = name;
        Location = location;
        Bio = bio;
        Image = image;
        Icon = icon;
        Experience = experience;
        Social = social;
    }
}

[Owned]
public class SocialLinks
{
    public string Instagram { get; set; }
    public string Facebook { get; set; }
    public string X { get; set; }

 
    public SocialLinks() { }

    public SocialLinks(string instagram, string facebook, string x)
    {
        Instagram = instagram;
        Facebook = facebook;
        X = x;
    }
}
