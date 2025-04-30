using BlogProject.Models.Services.ViewModels;

namespace BlogProject.Models.Services;

public interface IPostService
{
    List<PostViewModel> GetAll();

    CreatePostViewModel CreateViewModel();

    CreatePostViewModel CreateViewModel(CreatePostViewModel createPostViewModel);

    void Create(CreatePostViewModel createPostViewModel);
    
    PostViewModel? GetById(int id);
    EditPostViewModel? EditViewModel(int id);

    EditPostViewModel? EditViewModel(EditPostViewModel editPostViewModel);
    void Remove(int id);
    void Update(EditPostViewModel editPostViewModel);
    
    bool CanCurrentUserDelete(int postId, string? email);
    List<PostViewModel> GetAllPostsByCategory(int categoryId);
    List<PostViewModel> GetByCategoryId(int categoryId);

}