﻿namespace CoreModule.Application._Utilities;

public class CoreModuleDirectories
{
    public static string CvFileName = "wwwroot/core/teacher";
    public static string CourseImage = "wwwroot/core/course";
    public static string CourseDemo(Guid courseId) => $"/course/{courseId}";
    public static string GetCourseImage(string imageName) => $"{CourseImage.Replace("wwwroot","")}/{imageName}";
}