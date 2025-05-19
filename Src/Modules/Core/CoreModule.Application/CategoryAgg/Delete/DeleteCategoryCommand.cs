using Common.Application;
using CoreModules.Domain.CategoryAgg.Repositories;

namespace CoreModule.Application.CategoryAgg.Delete;

public record DeleteCategoryCommand(Guid CategoryId) : IBaseCommand;

public class DeleteCategoryCommandHandler:IBaseCommandHandler<DeleteCategoryCommand>
{
    private readonly ICourseCategoryRepository _repository;

    public DeleteCategoryCommandHandler(ICourseCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetTracking(request.CategoryId);
        if (category is null)
        {
            return OperationResult.NotFound();
        }

        await _repository.Delete(category);
        return OperationResult.Success();
    }
}