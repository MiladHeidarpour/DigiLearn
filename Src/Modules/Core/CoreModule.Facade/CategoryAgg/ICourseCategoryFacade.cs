using Common.Application;
using CoreModule.Application.CategoryAgg.AddChild;
using CoreModule.Application.CategoryAgg.Create;
using CoreModule.Application.CategoryAgg.Delete;
using CoreModule.Application.CategoryAgg.Edit;
using CoreModule.Query.CategoryAgg._Dtos;
using CoreModule.Query.CategoryAgg.GetById;
using CoreModule.Query.CategoryAgg.GetChildren;
using CoreModule.Query.CategoryAgg.GetList;
using MediatR;

namespace CoreModule.Facade.CategoryAgg;

public interface ICourseCategoryFacade
{
    Task<OperationResult> Create(CreateCategoryCommand command);
    Task<OperationResult> Edit(EditCategoryCommand command);
    Task<OperationResult> Delete(Guid id);
    Task<OperationResult> AddChild(AddCategoryChildCommand command);
    Task<CourseCategoryDto> GetById(Guid id);
    Task<List<CourseCategoryDto>> GetMainCategories();
    Task<List<CourseCategoryDto>> GetChildren(Guid parentId);
}

public class CourseCategoryFacade : ICourseCategoryFacade
{
    private readonly IMediator _mediator;

    public CourseCategoryFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> Create(CreateCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Delete(Guid id)
    {
        return await _mediator.Send(new DeleteCategoryCommand(id));
    }

    public async Task<OperationResult> AddChild(AddCategoryChildCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<CourseCategoryDto> GetById(Guid id)
    {
        return await _mediator.Send(new GetCourseCategoryByIdQuery(id));
    }

    public async Task<List<CourseCategoryDto>> GetMainCategories()
    {
        return await _mediator.Send(new GetCourseCategoryListQuery());
    }

    public async Task<List<CourseCategoryDto>> GetChildren(Guid parentId)
    {
        return await _mediator.Send(new GetCourseCategoryChildren(parentId));
    }
}