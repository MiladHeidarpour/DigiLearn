using BlogModule.Context;
using BlogModule.Domain;
using Common.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BlogModule.Repositories.Categories;

internal class CategoryRepository : BaseRepository<Category, BlogContext>, ICategoryRepository
{
    public CategoryRepository(BlogContext context) : base(context)
    {
    }

    public void Delete(Category category)
    {
        Context.Categories.Remove(category);
    }

    public async Task<List<Category>> GetAll()
    {
        return await Context.Categories.ToListAsync();
    }
}