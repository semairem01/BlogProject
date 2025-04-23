using BlogProject.Models.Services;
using BlogProject.Models.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers;

public class AuthController(IUserService userService) : Controller
{
    [HttpGet]
    public IActionResult CreateUser()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult CreateUser(CreateUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = userService.CreateUser(model);
            if (result) return RedirectToAction("SignIn", "Auth");
            ModelState.AddModelError("", "The user has not been created.");
        }

        return View(model);
    }
    
    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult SignIn(SignInViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = userService.SignIn(model);
            if (result) return RedirectToAction("Index", "Home");
            ModelState.AddModelError("", "Email or Password is incorrect.");
        }

        return View(model);
    }
    
    [HttpGet]
    public IActionResult SignOut()
    {
        userService.SignOut();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Register()
    {
        return View();
    }
    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}