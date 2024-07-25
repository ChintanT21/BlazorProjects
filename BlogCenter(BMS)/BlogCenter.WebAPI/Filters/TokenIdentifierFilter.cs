using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlogCenter.WebAPI.Filters
{
    public class TokenIdentifierFilter : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (IsAllowAnonymous(context))
            {
                await next();
                return;
            }
            var authService = context.HttpContext.RequestServices.GetService<IAuthService>();
            if (authService == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            string authorizationHeader = context.HttpContext.Request.Headers["Authorization"];
            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            {
                string token11 = authorizationHeader.Substring("Bearer ".Length).Trim();
                // Now 'token' contains your authorization token.
            }
            string token = context.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            TokenDto tokenDto = authService.GetUserDetailsByToken(token);
            if (tokenDto == null || !tokenDto.IsAuthenticated || tokenDto.ExpiryDate < DateTime.UtcNow)
            {
                context.Result = new UnauthorizedResult();
            }
            context.HttpContext.Items["TokenDto"] = tokenDto;
            await next();
        }

        private bool IsAllowAnonymous(ActionExecutingContext context)
        {
            if (context.ActionDescriptor is ControllerActionDescriptor actionDescriptor)
            {
                return actionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
                    .Any(a => a.GetType() == typeof(AllowAnonymousAttribute));
            }
            return false;
        }

    }
}
