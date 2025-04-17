using BlogModule.Domain;
using Common.Domain.Repository;

namespace BlogModule.Repositories.Categories;

internal interface ICategoryRepository : IBaseRepository<Category>
{
    void Delete(Category category);
    Task<List<Category>> GetAll();
}