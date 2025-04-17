using BlogModule.Context;
using BlogModule.Domain;
using Common.Infrastructure.Repository;

namespace BlogModule.Repositories.Posts;

internal class PostRepository : BaseRepository<Post, BlogContext>, IPostRepository
{
    public PostRepository(BlogContext context) : base(context)
    {
    }

    public void Delete(Post post)
    {
        Context.Posts.Remove(post);
    }
}