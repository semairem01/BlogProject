namespace BlogProject.Models.Repositories;

public interface IPostRepository
{
    List<Post> GetAll();
    Post? GetById(int id);
    void Add(Post post);
    void Update(Post post);
    void Delete(Post post);
}