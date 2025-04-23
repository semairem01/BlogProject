using BlogProject.Models.Services.Dtos;
using BlogProject.Models.Services.ViewModels;

namespace BlogProject.Models.Services;

public class PostApiService(HttpClient client)
{
    public List<PostViewModel> GetAll()
    {
        var response = client.GetAsync("/api/posts").Result;
        if (response.IsSuccessStatusCode)
        {
            var result = response.Content.ReadFromJsonAsync<List<PostDto>>().Result;

            var posts = new List<PostViewModel>();
            foreach (var item in result)
                posts.Add(new PostViewModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    Content = item.Content,
                    PublishDate = item.PublishDate,
                    Image = item.Image,
                    CategoryName = item.CategoryName,
                   
                });

            return posts;
        }

        throw new Exception("The data could not be retrieved from the API.");
    }
}