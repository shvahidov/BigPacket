using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.CustomAttributes;

public class RoleAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _role;

    public RoleAttribute(string role)
    {
        _role = role;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // 1. Получаем текущего пользователя
        var user = context.HttpContext.User;

        // 2. Проверяем, авторизован ли он
        if (!user.Identity?.IsAuthenticated ?? false)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // 3. Проверяем наличие роли
        var hasRole = user.IsInRole(_role);

        if (!hasRole)
        {
            context.Result = new ForbidResult();
        }
    }
}