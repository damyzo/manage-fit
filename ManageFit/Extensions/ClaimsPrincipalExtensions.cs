namespace ManageFit.Extensions
{
    using System.Security.Claims;

    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserEmail(this ClaimsPrincipal user) =>
            user.FindFirstValue("preferred_username")
            ?? user.FindFirstValue("email")
            ?? user.FindFirstValue(ClaimTypes.Email)
            ?? user.FindFirstValue("upn")
            ?? user.FindFirstValue(ClaimTypes.Upn)
            ?? user.FindFirstValue("unique_name")
            ?? string.Empty;
    }
}
