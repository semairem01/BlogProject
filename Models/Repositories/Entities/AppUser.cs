using Microsoft.AspNetCore.Identity;

namespace BlogProject.Models.Repositories.Entities;

public class AppUser : IdentityUser<Guid>
{
    public DateTime BirthDate { get; set; }
    public ICollection<Post>? Posts { get; set; } = new List<Post>();
}