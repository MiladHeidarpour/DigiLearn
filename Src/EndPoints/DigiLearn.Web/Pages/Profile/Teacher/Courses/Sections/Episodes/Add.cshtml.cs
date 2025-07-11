﻿using CoreModule.Application.CourseAgg.Episodes.Add;
using CoreModule.Facade.CourseAgg;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using DigiLearn.Web.Infrastructure.Utils.CustomValidation.IFormFile;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DigiLearn.Web.Pages.Profile.Teacher.Courses.Sections.Episodes;

[ServiceFilter(typeof(TeacherActionFilter))]
[BindProperties]
public class AddModel : BaseRazor
{
    private readonly ICourseFacade _facade;

    public AddModel(ICourseFacade facade)
    {
        _facade = facade;
    }


    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }

    [Display(Name = "عنوان انگلیسی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string EnglishTitle { get; set; }

    [Display(Name = "مدت زمان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [RegularExpression(@"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$", ErrorMessage = "لطفا زمان را با فرمت درست وارد کنید")]
    public TimeSpan TimeSpan { get; set; }

    [Display(Name = "ویدئو")]
    [FileType("mp4", ErrorMessage = "ویدئو نامعتبر است")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public IFormFile VideoFile { get; set; }

    [Display(Name = "فایل ضمیمه")]
    [FileType("rar", ErrorMessage = "فایل ضمیمه نامعتبر است")]
    public IFormFile? AttachmentFile { get; set; }

    [Display(Name = "قسمت رایگان")]
    public bool IsFree { get; set; }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(Guid courseId,Guid sectionId)
    {
        var result = await _facade.AddEpisode(new AddCourseEpisodeCommand()
        {
            Title = Title,
            AttachmentFile = AttachmentFile,
            VideoFile = VideoFile,
            IsActive = false,
            CourseId = courseId,
            EnglishTitle = EnglishTitle,
            TimeSpan = TimeSpan,
            SectionId = sectionId,
            IsFree = IsFree
        });
        return RedirectAndShowAlert(result, RedirectToPage("../Index", new { courseId }));
    }
}