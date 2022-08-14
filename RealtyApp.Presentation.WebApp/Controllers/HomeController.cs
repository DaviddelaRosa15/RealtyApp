using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Application.ViewModels.ImmovableAsset;
using RealtyApp.Presentation.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyApp.Presentation.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IImmovableAssetService _immovableAssetService;
        private readonly IImmovableAssetTypeService _immovableAssetTypeService;


        public HomeController(IImmovableAssetService immovableAssetService, IImmovableAssetTypeService immovableAssetTypeService)
        {
            _immovableAssetService = immovableAssetService;
            _immovableAssetTypeService = immovableAssetTypeService;
        }

        public async Task<IActionResult> Index(FilterViewModel vm)
        {
            ViewBag.DataFilterViewModel = await _immovableAssetService.GetDataFilterViewModel();
            ViewBag.ImmovableAssetTypes = await _immovableAssetTypeService.GetAllViewModelWithIncludes();
            ViewBag.AssetTypes = await _immovableAssetTypeService.GetAllViewModelWithIncludes();
            var model = await _immovableAssetService.GetAllViewModelWithFilters(vm);
            return View(model);
        }
    }
}
