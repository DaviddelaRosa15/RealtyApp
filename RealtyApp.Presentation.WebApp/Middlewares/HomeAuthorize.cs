using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using RealtyApp.Presentation.WebApp.Controllers;
using System.Linq;
using RealtyApp.Core.Application.Enums;

namespace RealtyApp.Presentation.WebApp.Middlewares
{
    public class HomeAuthorize : IAsyncActionFilter
    {
        private readonly ValidateUserSession _userSession;

        public HomeAuthorize(ValidateUserSession userSession)
        {
            _userSession = userSession;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = _userSession.HasUser();
            if (user != null)
            {
                var controller = (HomeController)context.Controller;
                if (user.Roles.Any(n => n == Roles.Administrator.ToString()))
                {
                    context.Result = controller.RedirectToAction("index", "Admin");
                }
                if (user.Roles.Any(n => n == Roles.Agent.ToString()))
                {
                    context.Result = controller.RedirectToAction("index", "Agent");
                }
                if (user.Roles.Any(n => n == Roles.Client.ToString()))
                {
                    context.Result = controller.RedirectToAction("index", "Client");
                }
            }
            else
            {
                await next();
            }
        }
    }
}
