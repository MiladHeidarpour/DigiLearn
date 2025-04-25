namespace UserModule.Core.Utilities;

public class UserDirectories
{
    public const string UserAvatar = "wwwroot/User/Images/Avatar";

    public static string GetUserAvatar(string imageName)
    {
        return $"{UserAvatar.Replace("wwwroot", "")}/{imageName}";
    }
}