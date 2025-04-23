using BlogProject.Models.Repositories.Entities;
using Microsoft.AspNetCore.Identity;

namespace BlogProject.Models.Repositories;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    
    public DateTime PublishDate { get; set; }
    public string? Image { get; set; }
    public int CategoryId { get; set; } 
    
    public Guid UserId { get; set; }
    public Category Category { get; set; } = null!;
    
    public AppUser AppUser { get; set; } = null!;

}