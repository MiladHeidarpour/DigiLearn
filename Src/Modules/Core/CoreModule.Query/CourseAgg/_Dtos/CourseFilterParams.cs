using Common.Query;
using Common.Query.Filter;
using CoreModule.Domain.CourseAgg.Enums;

namespace CoreModule.Query.CourseAgg._Dtos;

public class CourseFilterParams:BaseFilterParam
{
    public Guid? TeacherId { get; set; }
    public CourseActionStatus? ActionStatus { get; set; } = null;
}

public class CourseFilterData:BaseDto
{
    public string Title { get; set; }
    public string ImageName { get; set; }
    public string Slug { get; set; }
    public int Price { get; set; }
    public CourseActionStatus CourseActionStatus { get; set; }
    public int EpisodeCount { get; set; }
}

public class CourseFilterResult:BaseFilter<CourseFilterData,CourseFilterParams>
{

}