using Microsoft.AspNetCore.Mvc;
using RealtyApp.Core.Application.ViewModels.User;
using System.Threading.Tasks;
using RealtyApp.Core.Application.Helpers;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Application.Dtos.Account;
using RealtyApp.Presentation.WebApp.Middlewares;
using RealtyApp.Core.Application.Enums;
using System.IO;

namespace RealtyApp.Presentation.WebApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IUserService _userService;
        public ClientController(IUserService userService)
        {
            _userService = userService;
        }



    }
}

