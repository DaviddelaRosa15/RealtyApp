using Microsoft.AspNetCore.Http;
using RealtyApp.Core.Application.Dtos.Account;
using RealtyApp.Core.Application.Helpers;
using RealtyApp.Core.Application.ViewModels.User;

namespace RealtyApp.Presentation.WebApp.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public AuthenticationResponse HasUser()
        {
            AuthenticationResponse userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

            if (userViewModel == null)
            {
                return null;
            }
            return userViewModel;
        }

    }
}
