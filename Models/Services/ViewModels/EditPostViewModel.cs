using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogProject.Models.Services.ViewModels;

public class EditPostViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage ="Please enter the title of the post")]
    [Display(Name = "Title :")] 
    public string Title { get; set; } = null!;
    
    [Required(ErrorMessage = "Please enter the content of the post")]
    [Display(Name = "Content :")]
    public string Content { get; set; } = null!;
    
    
    [Display(Name = "Publish Date :")]
    public DateTime PublishDate { get; set; }
    
    public IFormFile? ImageFile { get; set; }
    public string? Image { get; set; } 
    
    [Required(ErrorMessage = "Please choose the category")]
    [Display(Name = "Category :")]
    public int? CategoryId { get; set; }
    
    [ValidateNever] public SelectList CategoryList { get; set; } = null!;
    
}