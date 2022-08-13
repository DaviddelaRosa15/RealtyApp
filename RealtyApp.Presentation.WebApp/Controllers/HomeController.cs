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
        private readonly ISellTypeService _sellTypeService;


        public HomeController(IImmovableAssetService immovableAssetService, IImmovableAssetTypeService immovableAssetTypeService, ISellTypeService sellTypeService)
        {
            _immovableAssetService = immovableAssetService;
            _immovableAssetTypeService = immovableAssetTypeService;
            _sellTypeService = sellTypeService;
        }

        public async Task<IActionResult> Index(FilterViewModel vm)
        {
            ViewBag.SellTypes = await _sellTypeService.GetAllViewModelWithIncludes();
            ViewBag.AssetTypes = await _immovableAssetTypeService.GetAllViewModelWithIncludes();
            var model = await _immovableAssetService.GetAllViewModelWithFilters(vm);
            return View(model);
        }
    }
}
