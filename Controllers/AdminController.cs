using BlogProject.Models.Repositories.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers;

public class AdminController(UserManager<AppUser> userManager, RoleManager<AppRole>roleManager) : Controller
{
    public IActionResult Index()
    {
        var roleToCreateResult = roleManager.CreateAsync(new AppRole { Name = "Admin" }).Result;
        
        if (roleToCreateResult.Succeeded)
        {
            var hasUser = userManager.FindByEmailAsync("semairem026@outlook.com").Result;

            if (hasUser is not null) userManager.AddToRoleAsync(hasUser, "Admin").Wait();
        }
        return View();
    }
}