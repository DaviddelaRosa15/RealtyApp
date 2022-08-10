using Microsoft.AspNetCore.Mvc;
using RealtyApp.Core.Application.ViewModels.User;
using System.Threading.Tasks;
using RealtyApp.Core.Application.Helpers;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Application.Dtos.Account;
using RealtyApp.Presentation.WebApp.Middlewares;
using System.IO;
using System;
using Microsoft.AspNetCore.Http;

namespace RealtyApp.Presentation.WebApp.Controllers
{
    public class AgentController : Controller
    {
        public AgentController(IUserService userService)
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

