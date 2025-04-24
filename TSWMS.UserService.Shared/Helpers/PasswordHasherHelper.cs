namespace TSWMS.UserService.Shared.Helpers;

public static class PasswordHasherHelper
{
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}
