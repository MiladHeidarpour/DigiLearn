﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Common.Domain.Utils;
using CoreModule.Application.CategoryAgg.Edit;
using CoreModule.Facade.CategoryAgg;
using DigiLearn.Web.Infrastructure.RazorUtils;

namespace DigiLearn.Web.Areas.Admin.Pages.Courses.Categories;

[BindProperties]
public class EditModel : BaseRazor
{
    private readonly ICourseCategoryFacade _categoryFacade;

    public EditModel(ICourseCategoryFacade categoryFacade)
    {
        _categoryFacade = categoryFacade;
    }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }

    [Display(Name = "عنوان انگلیسی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Slug { get; set; }
    public async Task<IActionResult> OnGet(Guid id)
    {
        var category = await _categoryFacade.GetById(id);
        if (category == null)
        {
            return RedirectToPage("Index");
        }


        Title=category.Title;
        Slug = category.Slug;
        return Page();
    }

    public async Task<IActionResult> OnPost(Guid id)
    {
        var result = await _categoryFacade.Edit(new EditCategoryCommand()
        {
            Id = id,
            Slug = Slug.ToSlug(),
            Title = Title
        });

        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}