using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.CustomAttributes;

public class RolesAttribute : Attribute, IAuthorizationFilter
{
    private readonly string[] _roles;

    public RolesAttribute(params string[]? roles)
    {
        _roles = roles ?? Array.Empty<string>();
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (user.Identity == null || !user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!_roles.Any(r => user.IsInRole(r)))
        {
            context.Result = new ForbidResult();
        }
    }
}