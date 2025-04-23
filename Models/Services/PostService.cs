using System.Security.Claims;
using BlogProject.Models.Repositories;
using BlogProject.Models.Services.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogProject.Models.Services;

public class PostService : IPostService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPostRepository _postRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public PostService(IPostRepository postRepository, ICategoryRepository categoryRepository,IHttpContextAccessor httpContextAccessor)
    {
        _postRepository = postRepository;
        _categoryRepository = categoryRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public List<PostViewModel> GetAll()
    {
        var postList= _postRepository.GetAll();
        var postViewModelList = new List<PostViewModel>();
        
        if (postList == null || !postList.Any())
        {
            return postViewModelList; // Return an empty list instead of null
        }
        
        foreach (var post in postList)
        {
            var postViewModel = new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                PublishDate = post.PublishDate,
                Image = post.Image,
                CategoryName = post.Category.Name,
                UserId = post.UserId
            };
            postViewModelList.Add(postViewModel);
        }
        return postViewModelList;
    }
    
    public List<PostViewModel> GetAllPostsByCategory(int categoryId)
    {
        var postList = _postRepository.GetAll().Where(p => p.CategoryId == categoryId).ToList();
        var postViewModelList = new List<PostViewModel>();

        foreach (var post in postList)
        {
            var postViewModel = new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                PublishDate = post.PublishDate,
                Image = post.Image,
                CategoryName = post.Category.Name,
                UserId = post.UserId
            };
            postViewModelList.Add(postViewModel);
        }

        return postViewModelList;
    }

    public CreatePostViewModel CreateViewModel()
    {
        var category = _categoryRepository.GetAll();
        var createPostViewModel = new CreatePostViewModel();
        createPostViewModel.CategoryList= new SelectList(category, "Id", "Name");
        return createPostViewModel;
    }

    public CreatePostViewModel CreateViewModel(CreatePostViewModel createPostViewModel)
    {
        var category = _categoryRepository.GetAll();
        createPostViewModel.CategoryList = new SelectList(category, "Id", "Name", createPostViewModel.CategoryId);
        return createPostViewModel;
    }
    
    public void Create(CreatePostViewModel createPostViewModel)
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);


        var post = new Post
        {
            Title = createPostViewModel.Title!,
            Content = createPostViewModel.Content,
            PublishDate = DateTime.Now,
            Image = createPostViewModel.Image,
            CategoryId = createPostViewModel.CategoryId!.Value,
            UserId = Guid.Parse(userId)
        };
        _postRepository.Add(post);
    }
    
    public PostViewModel? GetById(int id)
    {
        var post = _postRepository.GetById(id);

        if (post == null) return null!;
        var postViewModel = new PostViewModel
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            PublishDate = post.PublishDate,
            Image = post.Image,
            CategoryName = post.Category.Name
        };
        return postViewModel;
    }
    
    public void Remove(int id)
    {
        var post = _postRepository.GetById(id);
        if (post != null) _postRepository.Delete(post);
    }
    
    public EditPostViewModel? EditViewModel(int id)
    {
        var post = _postRepository.GetById(id);
        if (post == null) return null;
        var category = _categoryRepository.GetAll();
        var editPostViewModel = new EditPostViewModel
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            PublishDate = post.PublishDate,
            Image = post.Image,
            CategoryId = post.CategoryId
        };
        editPostViewModel.CategoryList = new SelectList(category, "Id", "Name", editPostViewModel.CategoryId);
        return editPostViewModel;
    }
    
    public void Update(EditPostViewModel editPostViewModel)
    {
        var post = _postRepository.GetById(editPostViewModel.Id);
        if (post == null) return;

        post.Title = editPostViewModel.Title!;
        post.Content = editPostViewModel.Content!;
        post.Image = editPostViewModel.Image;
        post.CategoryId = editPostViewModel.CategoryId!.Value;
        _postRepository.Update(post);
    }
    
    public EditPostViewModel? EditViewModel(EditPostViewModel editPostViewModel)
    {
        var category = _categoryRepository.GetAll();
        editPostViewModel.CategoryList = new SelectList(category, "Id", "Name", editPostViewModel.CategoryId);
        return editPostViewModel;
    }
    
    public bool CanCurrentUserDelete(int postId, string? currentUserId)
    {
        var post = _postRepository.GetById(postId);
        Console.WriteLine($"CurrentUserId: {currentUserId}");
        Console.WriteLine($"Post.UserId: {post?.UserId}");
        if (post == null || string.IsNullOrEmpty(currentUserId)) return false;

        if (!Guid.TryParse(currentUserId, out var userGuid))
        {
            Console.WriteLine("currentUserId is not a valid Guid.");
            return false; 
        }

        return post.UserId == userGuid;
    }
    
}