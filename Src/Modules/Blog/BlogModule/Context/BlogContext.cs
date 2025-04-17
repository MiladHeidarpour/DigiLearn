using BlogModule.Domain;
using Microsoft.EntityFrameworkCore;
namespace BlogModule.Context;

public class BlogContext : DbContext
{
    public BlogContext(DbContextOptions<BlogContext> option) : base(option)
    {

    }

    internal DbSet<Category> Categories { get; set; }
    internal DbSet<Post> Posts { get; set; }
}