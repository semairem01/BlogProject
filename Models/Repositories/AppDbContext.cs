using BlogProject.Models.Repositories.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Models.Repositories;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Post>(entity => 
            {
                
                entity.HasKey(p => p.Id);
                entity.Property(p=>p.Title).HasMaxLength(100).IsRequired();
                entity.Property(p=>p.Content).HasMaxLength(500).IsRequired();
                entity.Property(p=>p.PublishDate).IsRequired();
                entity.Property(p => p.Image).HasMaxLength(500);

                entity.HasOne(p => p.AppUser) // Post -> AppUser
                    .WithMany(u => u.Posts) // AppUser -> Posts
                    .HasForeignKey(p => p.UserId) // Foreign key'in hangi property olduğunu belirtiyoruz
                    .OnDelete(DeleteBehavior.Cascade);
                
                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Posts)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade); 
            });
        
        modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).HasMaxLength(100).IsRequired();
            });
        
        base.OnModelCreating(modelBuilder);
    }
}