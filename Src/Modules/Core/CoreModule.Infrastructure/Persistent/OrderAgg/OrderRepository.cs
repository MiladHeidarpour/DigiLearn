using Common.Infrastructure.Repository;
using CoreModule.Domain.OrderAgg.Models;
using CoreModule.Domain.OrderAgg.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Infrastructure.Persistent.OrderAgg;

class OrderRepository:BaseRepository<Order,CoreModuleEFContext>,IOrderRepository
{
    public OrderRepository(CoreModuleEFContext context) : base(context)
    {
    }

    public async Task<Order?> GetCurrentOrderByUserId(Guid userId)
    {
        return await Context.Orders.AsTracking().FirstOrDefaultAsync(f => f.IsPay == false && f.UserId == userId);
    }
}