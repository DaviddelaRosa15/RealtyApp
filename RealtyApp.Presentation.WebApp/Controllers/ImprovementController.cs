using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Application.ViewModels.Improvement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyApp.Presentation.WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ImprovementController : Controller
    {
        private readonly IImprovementService _improvementService;

        public ImprovementController(IImprovementService improvementService)
        {
            _improvementService = improvementService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _improvementService.GetAllViewModel());
        }
        public async Task<IActionResult> Create()
        {
            return View(new ImprovementSaveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ImprovementSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            await _improvementService.Add(vm);
            return RedirectToRoute(new { controller = "Improvement", action = "Index" });
        }
        public async Task<IActionResult> Edit(int id)
        {
            ImprovementSaveViewModel immoTypeSaveViewModel = await _improvementService.GetByIdSaveViewModel(id);

            return View("Create", immoTypeSaveViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ImprovementSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", vm);
            }
            await _improvementService.Update(vm, vm.Id);
            return RedirectToRoute(new { controller = "Improvement", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            ImprovementSaveViewModel immoTypeSaveViewModel = await _improvementService.GetByIdSaveViewModel(id);

            return View(immoTypeSaveViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ImprovementSaveViewModel vm)
        {
            await _improvementService.Delete(vm.Id);
            return RedirectToRoute(new { controller = "Improvement", action = "Index" });
        }



    }
}
