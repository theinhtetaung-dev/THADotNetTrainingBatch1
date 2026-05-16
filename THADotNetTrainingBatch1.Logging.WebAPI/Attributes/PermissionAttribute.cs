using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using THADotNetTrainingBatch1.Logging.WebAPI.Data;

namespace THADotNetTrainingBatch1.Logging.WebAPI.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class PermissionAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _permission;
    public bool CheckFromDb { get; set; } = false;

    public PermissionAttribute(string menu, string action)
    {
        _permission = $"{menu}.{action}";
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (!user.Identity?.IsAuthenticated ?? true)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // Step 1: Check from Token Claims
        var hasPermissionInToken = user.Claims.Any(c => 
            c.Type.Equals("Permission", StringComparison.OrdinalIgnoreCase) && 
            c.Value == _permission);

        if (!hasPermissionInToken)
        {
            context.Result = new ForbidResult();
            return;
        }

        // Step 2: Check from Database if enabled
        if (CheckFromDb)
        {
            var dbContext = context.HttpContext.RequestServices.GetRequiredService<AppDbContext>();
            var username = user.Identity?.Name ?? string.Empty;


            var hasPermissionInDb = dbContext.Users
                .Where(u => u.Username == username)
                .Join(dbContext.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => ur)
                .Join(dbContext.RolePermissions, ur => ur.RoleId, rp => rp.RoleId, (ur, rp) => rp)
                .Join(dbContext.Permissions, rp => rp.PermissionId, p => p.Id, (rp, p) => p)
                .Any(p => (p.Menu + "." + p.Action) == _permission);

            if (!hasPermissionInDb)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
