using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BlogProject.Models;
using BlogProject.Models.Repositories;
using BlogProject.Models.Services;

namespace BlogProject.Controllers;

public class HomeController : Controller
{
    private readonly IPostService _postService;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IPostService postService, ICategoryRepository categoryRepository)
    {
        _logger = logger;
        _postService = postService;
        _categoryRepository = categoryRepository;
    }

    public IActionResult Index()
    {
        var categories = _categoryRepository.GetAll();
        return View(categories);
    }
    
    public IActionResult PostsByCategory(int categoryId)
    {
        var posts = _postService.GetAllPostsByCategory(categoryId);
        var category = _categoryRepository.GetAll().FirstOrDefault(c => c.Id == categoryId);

        if (category == null)
        {
            return NotFound();
        }

        // Kategori adı ve postları birlikte View'a gönder
        ViewBag.CategoryName = category.Name;
        return View(posts);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    
}