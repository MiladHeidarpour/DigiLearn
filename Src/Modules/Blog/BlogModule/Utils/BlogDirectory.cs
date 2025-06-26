namespace BlogModule.Utils;

public class BlogDirectory
{
    public static string PostImage = "wwwroot/Blog/Images";
    public static string GetPostImage(string imageName) => $"{PostImage.Replace("wwwroot", "")}/{imageName}";
}