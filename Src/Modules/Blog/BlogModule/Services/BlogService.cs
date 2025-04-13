using AutoMapper;
using BlogModule.Domain;
using BlogModule.Repositories.Categories;
using BlogModule.Repositories.Posts;
using BlogModule.Services.Dtos.Commands;
using BlogModule.Services.Dtos.Queries;
using Common.Application;

namespace BlogModule.Services;

class BlogService : IBlogService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;

    public BlogService(ICategoryRepository categoryRepository, IMapper mapper, IPostRepository postRepository)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _postRepository = postRepository;
    }

    public async Task<OperationResult> CreateCategory(CreateCategoryCommand command)
    {
        var category = _mapper.Map<Category>(command);

        if (await _categoryRepository.ExistsAsync(f => f.Slug == category.Slug))
        {
            return OperationResult.Error("اسلاگ تکراری است");
        }

        await _categoryRepository.AddAsync(category);
        await _categoryRepository.Save();
        return OperationResult.Success();
    }

    public async Task<OperationResult> EditCategory(EditCategoryCommand command)
    {
        var category = await _categoryRepository.GetAsync(command.Id);

        if (category is null)
            return OperationResult.NotFound("دیتایی یافت نشد");


        if (command.Slug != category.Slug &&
            await _categoryRepository.ExistsAsync(f => f.Slug == category.Slug))
        {
            return OperationResult.Error("اسلاگ تکراری است");
        }

        category.Title = command.Title;
        category.Slug = command.Slug;

        _categoryRepository.Update(category);
        await _categoryRepository.Save();
        return OperationResult.Success();
    }

    public async Task<OperationResult> DeleteCategory(Guid categoryId)
    {
        var category = await _categoryRepository.GetAsync(categoryId);

        if (category is null)
            return OperationResult.NotFound("دیتایی یافت نشد");

        if (await _postRepository.ExistsAsync(f=>f.CategoryId==categoryId))
            return OperationResult.Error("این دسته بندی قبلا استفاده شده است،لطفا پست های مربوطه را حذف کنید و دوباره امتحان کنید");

        _categoryRepository.Delete(category);
        await _categoryRepository.Save();
        return OperationResult.Success();
    }

    public async Task<List<BlogCategoryDto>> GetAllCategories()
    {
        var categories = await _categoryRepository.GetAll();
        return _mapper.Map<List<BlogCategoryDto>>(categories);
    }

    public async Task<BlogCategoryDto> GetCategoryById(Guid categoryId)
    {
        var category = await _categoryRepository.GetAsync(categoryId);
        return _mapper.Map<BlogCategoryDto>(category);
    }
}