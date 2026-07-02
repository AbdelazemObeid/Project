using System.Security.Claims;

namespace project_MVC.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int? GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null) return null;
            var nameIdentifier = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(nameIdentifier, out int userId) ? userId : null;
        }

        public static string? GetUserName(this ClaimsPrincipal principal)
        {
            return principal?.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static string? GetUserEmail(this ClaimsPrincipal principal)
        {
            return principal?.FindFirst(ClaimTypes.Email)?.Value;
        }

        public static string? GetUserRole(this ClaimsPrincipal principal)
        {
            return principal?.FindFirst(ClaimTypes.Role)?.Value;
        }
    }
}
