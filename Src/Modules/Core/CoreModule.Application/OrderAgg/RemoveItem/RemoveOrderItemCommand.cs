using Common.Application;
using CoreModule.Domain.OrderAgg.Repositories;

namespace CoreModule.Application.OrderAgg.RemoveItem;

public record RemoveOrderItemCommand(Guid UserId, Guid CourseId) : IBaseCommand;

public class RemoveOrderItemCommandHandler : IBaseCommandHandler<RemoveOrderItemCommand>
{
    private readonly IOrderRepository _orderRepository;

    public RemoveOrderItemCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult> Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetCurrentOrderByUserId(request.UserId);
        if (order == null)
        {
            return OperationResult.NotFound();
        }

        order.RemoveItem(request.CourseId);
        await _orderRepository.Save();
        return OperationResult.Success();
    }
}