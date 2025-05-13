using Common.Domain;
using Common.Domain.ValueObjects;
using CoreModules.Domain.Course.Enums;

namespace CoreModules.Domain.Course.Entities;

public class Course:BaseEntity
{
    public Course(Guid teacherId, string title, string description, string imageName, string videoName, int price, DateTime lastUpdate, CourseLevel courseLevel, SeoData seoData)
    {
        TeacherId = teacherId;
        Title = title;
        Description = description;
        ImageName = imageName;
        VideoName = videoName;
        Price = price;
        LastUpdate = lastUpdate;
        CourseLevel = courseLevel;
        SeoData = seoData;
        CourseStatus = CourseStatus.StartSoon;
    }

    public Guid TeacherId { get;private set; }
    public string Title { get;private set; }
    public string Description { get;private set; }
    public string ImageName { get;private set; }
    public string VideoName { get;private set; }
    public int Price { get;private set; }
    public DateTime LastUpdate { get;private set; }
    public CourseLevel CourseLevel { get; set; }
    public CourseStatus CourseStatus { get; set; }
    public SeoData SeoData { get;private set; }

    public IEnumerable<Section> Sections { get; }
}