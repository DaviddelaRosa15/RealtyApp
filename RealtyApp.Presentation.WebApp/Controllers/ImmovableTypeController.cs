
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Application.ViewModels.ImmovableAssetType;
using System.Threading.Tasks;

namespace RealtyApp.Presentation.WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ImmovableTypeController : Controller
    {       
        private readonly IImmovableAssetTypeService _immovableAssetType;

        public ImmovableTypeController( IImmovableAssetTypeService immovableAssetTypeService)
        {
            _immovableAssetType = immovableAssetTypeService;
        }
        
        public async Task< IActionResult> Index()
        {          
            return View(await _immovableAssetType.GetAllWithCountTypeImmovableUse());
        }
        public async Task<IActionResult> Create()
        {            
            return View(new ImmovableAssetTypeSaveViewModel ());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ImmovableAssetTypeSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            await _immovableAssetType.Add(vm);
            return RedirectToRoute(new { controller = "ImmovableType", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            ImmovableAssetTypeSaveViewModel immoTypeSaveViewModel = await _immovableAssetType.GetByIdSaveViewModel(id);
         
            return View("Create",immoTypeSaveViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ImmovableAssetTypeSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Create",vm);
            }
            await _immovableAssetType.Update(vm, vm.Id);
            return RedirectToRoute(new { controller = "ImmovableType", action = "Index" });
        }
        public async Task<IActionResult> Delete(int id)
        {
            ImmovableAssetTypeSaveViewModel immoTypeSaveViewModel = await _immovableAssetType.GetByIdSaveViewModel(id);

            return View( immoTypeSaveViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ImmovableAssetTypeSaveViewModel vm)
        {
            await _immovableAssetType.Delete(vm.Id);
            return RedirectToRoute(new { controller = "ImmovableType", action = "Index" });
        }

    }
}
