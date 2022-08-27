using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Application.ViewModels.SellType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyApp.Presentation.WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SellTypeController : Controller
    {

        private readonly    ISellTypeService _isellType;

        public SellTypeController(ISellTypeService isellType)
        {
            _isellType = isellType;
        }

        public async Task< IActionResult >Index()
        {
            return View( await _isellType.GetAllWithSellType());
        }

        public IActionResult Create()
        {
            return View(new SaveSellTypeViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveSellTypeViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            await _isellType.Add(vm);
            return RedirectToRoute(new { controller = "SellType", action = "Index" });
        }
        public async Task<IActionResult> Edit(int id)
        {
            SaveSellTypeViewModel immoTypeSaveViewModel = await _isellType.GetByIdSaveViewModel(id);

            return View("Create", immoTypeSaveViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SaveSellTypeViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", vm);
            }
            await _isellType.Update(vm, vm.Id);
            return RedirectToRoute(new { controller = "SellType", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            SaveSellTypeViewModel immoTypeSaveViewModel = await _isellType.GetByIdSaveViewModel(id);

            return View(immoTypeSaveViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(SaveSellTypeViewModel vm)
        {
            await _isellType.Delete(vm.Id);
            return RedirectToRoute(new { controller = "SellType", action = "Index" });
        }


    }
}
