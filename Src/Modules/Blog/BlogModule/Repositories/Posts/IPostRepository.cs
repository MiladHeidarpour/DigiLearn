using BlogModule.Domain;
using Common.Domain.Repository;

namespace BlogModule.Repositories.Posts;

internal interface IPostRepository : IBaseRepository<Post>
{
    void Delete(Post post);
}