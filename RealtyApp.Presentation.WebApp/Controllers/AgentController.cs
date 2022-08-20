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
using Microsoft.AspNetCore.Authorization;
using RealtyApp.Core.Application.ViewModels.ImmovableAsset;

namespace RealtyApp.Presentation.WebApp.Controllers
{
    [Authorize(Roles = "Agent")]
    public class AgentController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AuthenticationResponse _loggedUser;
        private readonly IImmovableAssetService _immovableAssetService;
        private readonly IImmovableAssetTypeService _immovableAssetTypeService;
        private readonly ISellTypeService _sellTypeService;

        public AgentController(IHttpContextAccessor contextAccessor, IImmovableAssetService immovableAssetService, IImmovableAssetTypeService immovableAssetTypeService, ISellTypeService sellTypeService)
        {
            _contextAccessor = contextAccessor;
            _loggedUser = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _immovableAssetService = immovableAssetService;
            _immovableAssetTypeService = immovableAssetTypeService;
            _sellTypeService = sellTypeService;
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Index(FilterViewModel vm, string id = null)
        {
            ViewBag.DataFilterViewModel = await _immovableAssetService.GetDataFilterViewModel();
            ViewBag.ImmovableAssetTypes = await _immovableAssetTypeService.GetAllViewModelWithIncludes();
            ViewBag.AssetTypes = await _immovableAssetTypeService.GetAllViewModelWithIncludes();
            
            var model = await _immovableAssetService.GetAllViewModelWithFilters(vm, _loggedUser.Id);
            return View(model);
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> MyImmovables(FilterViewModel vm, string id = null)
        {
            ViewBag.DataFilterViewModel = await _immovableAssetService.GetDataFilterViewModel();
            ViewBag.ImmovableAssetTypes = await _immovableAssetTypeService.GetAllViewModelWithIncludes();
            ViewBag.AssetTypes = await _immovableAssetTypeService.GetAllViewModelWithIncludes();

            var model = await _immovableAssetService.GetAllViewModelWithFilters(vm, _loggedUser.Id);
            return View(model);
        }


        public async Task<IActionResult> AddImmovable(SaveImmovableAssetViewModel saveImmovableAsset)
        {
            ViewBag.ImmovableAssetTypes = await _immovableAssetTypeService.GetAllViewModel();
            ViewBag.ImmovableSellTypes = await _sellTypeService.GetAllViewModel();


            return View("Immovables",saveImmovableAsset);
        }

        public IActionResult MyProfile()
        {
            return View();
        }


    }
}

