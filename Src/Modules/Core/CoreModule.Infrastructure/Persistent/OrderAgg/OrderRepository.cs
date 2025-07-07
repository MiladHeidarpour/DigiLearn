using Common.Infrastructure.Repository;
using CoreModule.Domain.OrderAgg.Models;
using CoreModule.Domain.OrderAgg.Repositories;

namespace CoreModule.Infrastructure.Persistent.OrderAgg;

class OrderRepository:BaseRepository<Order,CoreModuleEFContext>,IOrderRepository
{
    public OrderRepository(CoreModuleEFContext context) : base(context)
    {
    }
}