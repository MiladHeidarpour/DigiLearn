﻿using CoreModule.Domain.CourseAgg.Enums;
using CoreModule.Facade.CourseAgg;
using CoreModule.Query.CourseAgg._Dtos;
using DigiLearn.Web.ViewModels;

namespace DigiLearn.Web.Infrastructure.Services;

public interface IHomePageService
{
    Task<HomePageViewModel> GetData();
}

public class HomePageService : IHomePageService
{
    private readonly ICourseFacade _courseFacade;

    public HomePageService(ICourseFacade courseFacade)
    {
        _courseFacade = courseFacade;
    }

    public async Task<HomePageViewModel> GetData()
    {
        var courses = await _courseFacade.GetByFilter(new CourseFilterParams()
        {
            Take = 8,
            ActionStatus = CourseActionStatus.Active,
            PageId = 1,
            FilterSort = CourseFilterSort.Latest,
        });
        var model = new HomePageViewModel()
        {
            LatestCourses = courses.Data.Select(s=>new CourseCardViewModel
            {
                Title = s.Title,
                Slug = s.Slug,
                ImageName = s.ImageName,
                Price = s.Price,
                Duration = "",
                Visit = 0,
                CommentCounts = 0,
                TeacherName = s.TeacherName
            }).ToList()
        };
        return model;
    }
}