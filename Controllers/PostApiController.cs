using BlogProject.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers;

public class PostApiController(PostApiService postApiService) : Controller
{
    public IActionResult Index()
    {
        ViewBag.posts = postApiService.GetAll();
        return View();
    }
}