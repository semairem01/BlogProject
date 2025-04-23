using Microsoft.EntityFrameworkCore;

namespace BlogProject.Models.Repositories;

public class PostRepository : IPostRepository
{
    private readonly AppDbContext _context;

    public PostRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Post> GetAll()
    {
        return _context.Posts
            .Include(p => p.Category)
            .Include(p => p.AppUser) // Bu sayede User bilgileri de alınır
            .ToList();
    }

    public Post? GetById(int id)
    {
        return _context.Posts.Find(id);
    }

    public void Add(Post post)
    {
        _context.Posts.Add(post);
        _context.SaveChanges();
    }

    public void Update(Post post)
    {
        _context.Posts.Update(post);
        _context.SaveChanges();
    }

    public void Delete(Post post)
    {
        _context.Posts.Remove(post);
        _context.SaveChanges();
    }
    
}