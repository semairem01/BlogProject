using System.Security.Claims;
using BlogProject.Models.Services;
using BlogProject.Models.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers;

public class PostController(IPostService postService) : Controller
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        var viewModelList = postService.GetAll();
        ViewBag.postList = viewModelList;
        return View(viewModelList);
    }

    
    [Authorize]
    [HttpGet]
    public IActionResult Create()
    {
        var createPostViewModel = postService.CreateViewModel();
        
        return View(createPostViewModel);
    }

    [Authorize]
    [HttpPost]
    public IActionResult Create(CreatePostViewModel createPostViewModel)
    {
        if (!ModelState.IsValid)
            return View(createPostViewModel); // Eski modeli geri döndür, dosya alanı da korunur

        if (createPostViewModel.ImageFile != null)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(createPostViewModel.ImageFile.FileName);
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                createPostViewModel.ImageFile.CopyTo(stream);
            }

            createPostViewModel.Image = "/images/" + fileName;
        }

        postService.Create(createPostViewModel);
        return RedirectToAction("Index");
        
    }

   
    [Authorize]
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Guid
        if (!postService.CanCurrentUserDelete(id, currentUserId))
            return Forbid();

        postService.Remove(id);
        return RedirectToAction("Index");
    }
    
    [Authorize]
    [HttpGet]
    public IActionResult Edit(int id)
    {
        
        return View(postService.EditViewModel(id));
    }
    
    
    [Authorize]
    [HttpPost]
    public IActionResult Edit(EditPostViewModel editPostViewModel)
    {
        if (!ModelState.IsValid) return View(postService.EditViewModel(editPostViewModel));
        
        if (editPostViewModel.ImageFile != null)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(editPostViewModel.ImageFile.FileName);
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                editPostViewModel.ImageFile.CopyTo(stream);
            }

            editPostViewModel.Image = "/images/" + fileName;
        }
        
        postService.Update(editPostViewModel);
        return RedirectToAction("Index");
    }
}