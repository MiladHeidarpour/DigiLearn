using CoreModule.Domain.CourseAgg.Repositories;
using CoreModule.Domain.OrderAgg.DomainServices;

namespace CoreModule.Application.OrderAgg;

public class OrderDomainService:IOrderDomainService
{
    private readonly ICourseRepository _courseRepository;

    public OrderDomainService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<int> GetCoursePriceById(Guid courseId)
    {
        var course = await _courseRepository.GetAsync(courseId);
        if (course is null || course.Price is 0)
        {
            return 0;
        }

        return course.Price;
    }
}