using BlogModule.Domain;
using Microsoft.EntityFrameworkCore;
namespace BlogModule.Context;

class BlogContext : DbContext
{
    public BlogContext(DbContextOptions<BlogContext> option) : base(option)
    {

    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Post> Posts { get; set; }
}