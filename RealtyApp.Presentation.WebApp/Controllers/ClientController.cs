using Microsoft.AspNetCore.Mvc;
using RealtyApp.Core.Application.ViewModels.User;
using System.Threading.Tasks;
using RealtyApp.Core.Application.Helpers;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Application.Dtos.Account;
using RealtyApp.Presentation.WebApp.Middlewares;
using RealtyApp.Core.Application.Enums;
using System.IO;
using RealtyApp.Core.Application.ViewModels.ImmovableAsset;

namespace RealtyApp.Presentation.WebApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IImmovableAssetService _immovableAssetService;
        private readonly IImmovableAssetTypeService _immovableAssetTypeService;
        private readonly IUserService _userService;

        public ClientController(IUserService userService, IImmovableAssetService immovableAssetService, IImmovableAssetTypeService immovableAssetTypeService)
        {
            _immovableAssetService = immovableAssetService;
            _immovableAssetTypeService = immovableAssetTypeService;
            _userService = userService;
        }

        public async Task<IActionResult> Index(FilterViewModel vm, string id = null)
        {
            ViewBag.DataFilterViewModel = await _immovableAssetService.GetDataFilterViewModel();
            ViewBag.ImmovableAssetTypes = await _immovableAssetTypeService.GetAllViewModelWithIncludes();
            ViewBag.AssetTypes = await _immovableAssetTypeService.GetAllViewModelWithIncludes();
            var model = await _immovableAssetService.GetAllViewModelWithFilters(vm, id);
            return View(model);
        }

        public async Task<IActionResult> Agents()
        {
            var model = await _userService.GetAllUserAgentAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Agents(string agentName)
        {
            var model = await _userService.GetUserAgentByName(agentName);
            return View(model);
        }
    }
}

