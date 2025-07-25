﻿using Common.Query;
using CoreModule.Domain.TeacherAgg.Enums;
using CoreModule.Query._Data;
using CoreModule.Query._Dtos;
using CoreModule.Query.CategoryAgg._Dtos;
using CoreModule.Query.CourseAgg._Dtos;
using CoreModule.Query.TeacherAgg._Dtos;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.CourseAgg.GetBySlug;

public record GetCourseBySlugQuery(string Slug) : IQuery<CourseDto?>;

class GetCourseBySlugQueryHandler : IQueryHandler<GetCourseBySlugQuery, CourseDto?>
{
    private readonly QueryContext _context;
    public GetCourseBySlugQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<CourseDto?> Handle(GetCourseBySlugQuery request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses
            .Include(c => c.Category)
            .Include(c => c.SubCategory)
            .Include(c => c.Teacher.User)
            .Include(c => c.Sections)
            .ThenInclude(c => c.Episodes)
            .FirstOrDefaultAsync(f => f.Slug == request.Slug, cancellationToken: cancellationToken);

        if (course == null)
        {
            return null;
        }

        return new CourseDto()
        {
            Id = course.Id,
            CreationDate = course.CreationDate,
            TeacherId = course.TeacherId,
            CategoryId = course.CategoryId,
            SubCategoryId = course.SubCategoryId,
            Title = course.Title,
            Slug = course.Slug,
            Description = course.Description,
            ImageName = course.ImageName,
            VideoName = course.VideoName,
            Price = course.Price,
            LastUpdate = course.LastUpdate,
            SeoData = course.SeoData,
            CourseLevel = course.CourseLevel,
            CourseStatus = course.CourseStatus,
            ActionStatus = course.Status,
            Sections = course.Sections.Select(s => new CourseSectionDto()
            {
                Title = s.Title,
                Id = s.Id,
                CourseId = s.CourseId,
                CreationDate = s.CreationDate,
                DisplayOrder = s.DisplayOrder,
                Episodes = s.Episodes.Select(r => new EpisodeDto
                {
                    Id = r.Id,
                    CreationDate = r.CreationDate,
                    SectionId = r.SectionId,
                    Title = r.Title,
                    EnglishTitle = r.EnglishTitle,
                    Token = r.Token,
                    TimeSpan = r.TimeSpan,
                    VideoName = r.VideoName,
                    AttachmentName = r.AttachmentName,
                    IsActive = r.IsActive,
                    IsFree = r.IsFree
                }).ToList(),
            }).ToList(),
            Teacher = new TeacherDto
            {
                Id = course.Teacher.Id,
                CreationDate = course.Teacher.CreationDate,
                UserName = course.Teacher.UserName,
                CvFileName = "",
                Status = course.Teacher.Status,
                User = new CoreModuleUserDto
                {
                    Id = course.Teacher.User.Id,
                    CreationDate = course.Teacher.User.CreationDate,
                    Name = course.Teacher.User.Name,
                    Family = course.Teacher.User.Family,
                    PhoneNumber = course.Teacher.User.PhoneNumber,
                    Email = course.Teacher.User.Email,
                    Avatar = course.Teacher.User.Avatar
                }
            },
            MainCategory = new CourseCategoryDto
            {
                Id = course.Category.Id,
                CreationDate = course.Category.CreationDate,
                Title = course.Category.Title,
                Slug = course.Category.Slug,
                ParentId = course.Category.ParentId,
                Children = null
            },
            SubCategory = new CourseCategoryDto
            {
                Id = course.SubCategory.Id,
                CreationDate = course.SubCategory.CreationDate,
                Title = course.SubCategory.Title,
                Slug = course.SubCategory.Slug,
                ParentId = course.SubCategory.ParentId,
                Children = null
            }
        };
    }
}
