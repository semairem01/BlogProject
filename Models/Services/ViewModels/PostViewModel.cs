namespace BlogProject.Models.Services.ViewModels;

public class PostViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime PublishDate { get; set; }
    public string? Image { get; set; }
    public string CategoryName { get; set; } = null!;
    
    public Guid UserId { get; set; }
    
}