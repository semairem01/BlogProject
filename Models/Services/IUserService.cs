using BlogProject.Models.Services.ViewModels;

namespace BlogProject.Models.Services;

public interface IUserService
{
    bool CreateUser(CreateUserViewModel model);
    bool SignIn(SignInViewModel model);

    void SignOut();
}