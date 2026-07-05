using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace project_MVC.Extensions
{
    public static class ControllerExtensions
    {
        public static Guid GetCurrentUserId(this Controller controller)
        {
            var claim = controller.User.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? Guid.Parse(claim.Value) : Guid.Empty;
        }
    }
}
