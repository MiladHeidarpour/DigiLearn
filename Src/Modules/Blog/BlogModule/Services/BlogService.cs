﻿using AutoMapper;
using BlogModule.Domain;
using BlogModule.Repositories.Categories;
using BlogModule.Repositories.Posts;
using BlogModule.Services.Dtos.Commands;
using BlogModule.Services.Dtos.Queries;
using BlogModule.Utils;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.SecurityUtil;

namespace BlogModule.Services;

class BlogService : IBlogService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;
    private readonly ILocalFileService _localFileService;

    public BlogService(ICategoryRepository categoryRepository, IMapper mapper, IPostRepository postRepository, ILocalFileService localFileService)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _postRepository = postRepository;
        _localFileService = localFileService;
    }

    public async Task<OperationResult> CreateCategory(CreateCategoryCommand command)
    {
        var category = _mapper.Map<Category>(command);

        if (await _categoryRepository.ExistsAsync(f => f.Slug == category.Slug))
            return OperationResult.Error("اسلاگ تکراری است");

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

        if (await _postRepository.ExistsAsync(f => f.CategoryId == categoryId))
            return OperationResult.Error("این دسته بندی قبلا استفاده شده است،لطفا پست های مربوطه را حذف کنید و دوباره امتحان کنید");

        _categoryRepository.Delete(category);
        await _categoryRepository.Save();
        return OperationResult.Success();
    }

    public async Task<OperationResult> CreatePost(CreatePostCommand command)
    {
        var post = _mapper.Map<Post>(command);
        if (await _postRepository.ExistsAsync(f => f.Slug == post.Slug))
            return OperationResult.Error("اسلاگ تکراری است");

        if (command.ImageFile.IsImage() == false)
            return OperationResult.Error("عکس وارد شده نامعتبر است");

        var imageName = await _localFileService.SaveFileAndGenerateName(command.ImageFile, BlogDirectory.PostImage);
        post.ImageName=imageName;
        post.Visit = 1;
        post.Description = command.Description.SanitizeText();


        await _postRepository.AddAsync(post);
        await _postRepository.Save();
        return OperationResult.Success();
    }

    public async Task<OperationResult> EditPost(EditPostCommand command)
    {
        var post = await _postRepository.GetTracking(command.Id);
        if (post is null)
            return OperationResult.NotFound("اطلاعات یافت نشد");

        if (command.Slug != post.Slug &&
            await _postRepository.ExistsAsync(f => f.Slug == post.Slug))
        {
            return OperationResult.Error("اسلاگ تکراری است");
        }

        if (command.ImageFile !=null)
        {
            if (command.ImageFile.IsImage() == false)
            {
                return OperationResult.Error("عکس وارد شده نامعتبر است");
            }
            else
            {
                var imageName = await _localFileService.SaveFileAndGenerateName(command.ImageFile, BlogDirectory.PostImage);
                post.ImageName= imageName;
            }
        }

        post.Description = command.Description.SanitizeText();
        post.OwnerName=command.OwnerName;
        post.Title = command.Title;
        post.CategoryId = command.CategoryId;
        post.UserId = command.UserId;
        post.Slug = command.Slug;

        await _postRepository.Save();
        return OperationResult.Success();
        
    }

    public async Task<OperationResult> DeletePost(Guid postId)
    {
        var post = await _postRepository.GetAsync(postId);

        if (post is null)
            return OperationResult.NotFound("اطلاعات یافت نشد");

        _postRepository.Delete(post);
        await _postRepository.Save();
        _localFileService.DeleteFile(BlogDirectory.PostImage,post.ImageName);
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

    public async Task<BlogPostDto?> GetPostById(Guid postId)
    {
        var post = await _postRepository.GetAsync(postId);
        if (post is null)
            return null;

        return _mapper.Map<BlogPostDto>(post);
    }
}