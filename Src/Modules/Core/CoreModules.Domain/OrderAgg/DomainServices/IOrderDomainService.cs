namespace CoreModule.Domain.OrderAgg.DomainServices;

public interface IOrderDomainService
{
    Task<int> GetCoursePriceById(Guid courseId);
}