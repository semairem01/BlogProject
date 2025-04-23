using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogProject.Models.Services.ViewModels;

public class CreatePostViewModel
{
    
    [Required(ErrorMessage ="Please enter the title of the post")]
    [Display(Name = "Title :")] 
    public string Title { get; set; } = null!;
    
    [Required(ErrorMessage = "Please enter the content of the post")]
    [Display(Name = "Content :")]
    public string Content { get; set; } = null!;
    
    public IFormFile? ImageFile { get; set; }
    public string? Image { get; set; }
    
    [Required(ErrorMessage = "Please choose the category")]
    [Display(Name = "Category :")]
    public int? CategoryId { get; set; }
    
    [ValidateNever] public SelectList CategoryList { get; set; } = null!;
}