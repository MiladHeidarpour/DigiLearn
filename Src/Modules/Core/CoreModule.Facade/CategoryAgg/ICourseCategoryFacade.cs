using Common.Application;
using CoreModule.Application.CategoryAgg.AddChild;
using CoreModule.Application.CategoryAgg.Create;
using CoreModule.Application.CategoryAgg.Delete;
using CoreModule.Application.CategoryAgg.Edit;
using MediatR;

namespace CoreModule.Facade.CategoryAgg;

public interface ICourseCategoryFacade
{
    Task<OperationResult> Create(CreateCategoryCommand command);
    Task<OperationResult> Edit(EditCategoryCommand command);
    Task<OperationResult> Delete(DeleteCategoryCommand command);
    Task<OperationResult> AddChild(AddCategoryChildCommand command);
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

    public async Task<OperationResult> Delete(DeleteCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddChild(AddCategoryChildCommand command)
    {
        return await _mediator.Send(command);
    }
}