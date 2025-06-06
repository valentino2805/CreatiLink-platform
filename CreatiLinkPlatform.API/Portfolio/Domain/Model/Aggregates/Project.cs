using CreatiLinkPlatform.API.Profile.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace CreatiLinkPlatform.API.Projects.Domain.Model.Aggregates;

public class Project
{
    public int Id { get; set; }
    public string Title { get; private set; }
    public string Image { get; private set; }
    public string Likes { get; private set; }
    public string Comments { get; private set; }
    public string Description { get; private set; }
    public List<string> Technologies { get; private set; }

    public int ProfileId { get; private set; }
    public Profile.Domain.Model.Aggregates.Profile? Profile { get; private set; }


    private Project()
    {
        Technologies = new List<string>();
    }

    // Constructor actualizado para generar likes y comments automáticamente
    public Project(int profileId, string title, string image, string description, List<string> technologies)
    {
        ProfileId = profileId;
        Title = title;
        Image = image;
        Description = description;
        Technologies = technologies ?? new List<string>();

        var rand = new Random();
        Likes = rand.Next(0, 500).ToString();
        Comments = rand.Next(0, 100).ToString();
    }

    public void UpdateProject(string title, string image, string description, List<string> technologies)
    {
        Title = title;
        Image = image;
        Description = description;
        Technologies = technologies ?? new List<string>();

        // Opcional: podrías decidir si los likes y comments cambian o no en update
        // Likes = rand.Next(0, 500).ToString(); // si quieres regenerarlos en updates también
        // Comments = rand.Next(0, 100).ToString();
    }
}