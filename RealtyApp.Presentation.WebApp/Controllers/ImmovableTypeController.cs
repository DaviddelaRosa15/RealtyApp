
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
            return View(await _immovableAssetType.GetAllViewModel());
        }
        public async Task<IActionResult> Create()
        {
            return View();
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
    }
}
