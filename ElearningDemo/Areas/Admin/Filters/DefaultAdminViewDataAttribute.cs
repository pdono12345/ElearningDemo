using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Text;

namespace ElearningDemo.Areas.Admin.Filters
{
    public class DefaultAdminViewDataAttribute : Attribute, IAsyncActionFilter
    {
        private readonly IManagerService _managerService;
        private readonly IHttpContextAccessor _httpContextAccessor = null!;

        public DefaultAdminViewDataAttribute(IManagerService managerService, IHttpContextAccessor httpContextAccessor)
        {
            _managerService = managerService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Do something before the action executes.
            await next();
            // Do something after the action executes.

            if (_httpContextAccessor.HttpContext is null) { context.Result = new UnauthorizedResult(); }

            if (context.Controller is not Controller) { context.Result = new BadRequestResult(); }

            var sid = context.HttpContext.User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Sid);
            if (sid is null) { context.Result = new UnauthorizedResult(); }

            if (context.Controller is Controller controller &&
                _httpContextAccessor.HttpContext is not null &&
                sid is not null)
            {
                int.TryParse(sid.Value, out int managerIdFromSid);
                var manager = await _managerService.GetManagerByIdAsync(managerIdFromSid);

                if (manager is null) { context.Result = new UnauthorizedResult(); }
                if (manager is not null)
                {
                    // Set common infomation for razor view
                    controller.ViewData["ManagerId"] = manager.Id;
                    controller.ViewData["ManagerName"] = manager.Name;
                    controller.ViewData["baseUrl"] = GetBaseUrl(_httpContextAccessor.HttpContext.Request);
                    controller.ViewData["Reffer"] = context.HttpContext.Request.Headers["Referer"].ToString();
                }
            }
        }

        private string GetBaseUrl(HttpRequest request)
        {
            string baseUrl = new StringBuilder()
                .Append(request.Scheme)
                .Append("://")
                .Append(request.Host)
                .Append(request.PathBase)
                .ToString();
            return baseUrl;
        }
    }
}
