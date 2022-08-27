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
using Microsoft.AspNetCore.Http;
using RealtyApp.Core.Application.ViewModels.FavoriteImmovable;

namespace RealtyApp.Presentation.WebApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IImmovableAssetService _immovableAssetService;
        private readonly IImmovableAssetTypeService _immovableAssetTypeService;
        private readonly IFavoriteImmovableService _favoriteImmovableService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse _loggedUser;
        private readonly IUserService _userService;

        public ClientController(IUserService userService, IImmovableAssetService immovableAssetService,
            IImmovableAssetTypeService immovableAssetTypeService, IFavoriteImmovableService favoriteImmovableService, IHttpContextAccessor httpContextAccessor)
        {
            _immovableAssetService = immovableAssetService;
            _immovableAssetTypeService = immovableAssetTypeService;
            _httpContextAccessor = httpContextAccessor;
            _loggedUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _userService = userService;
            _favoriteImmovableService = favoriteImmovableService;
        }

        public async Task<IActionResult> Index(FilterViewModel vm, string id = null)
        {
            var DataFilterViewModel = await _immovableAssetService.GetDataFilterViewModel();
            ViewBag.DataFilterViewModel = DataFilterViewModel;
            ViewBag.ImmovableAssetTypes = await _immovableAssetTypeService.GetAllViewModelWithIncludes();
            ViewBag.AssetTypes = await _immovableAssetTypeService.GetAllViewModelWithIncludes();
            if (vm.MinPrice > 0 && (vm.MaxPrice == null || vm.MaxPrice == 0))
            {
                vm.MaxPrice = DataFilterViewModel.MaxPrice;
            }
            var model = await _immovableAssetService.GetAllViewModelWithFilters(id, vm);
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

        public async Task<IActionResult> ManageFavoriteImmovable(int id, string idClient)
        {
            SaveFavoriteImmovableViewModel save = new()
            {
                ImmovableAssetId = id,
                ClientId = idClient
            };
            await _favoriteImmovableService.ManageFavoriteImmovable(save);
            return RedirectToRoute(new { controller = "Client", action = "Index" });
        }

        public async Task<IActionResult> MyFavorites(FilterViewModel vm)
        {
            var DataFilterViewModel = await _immovableAssetService.GetDataFilterViewModel();
            ViewBag.DataFilterViewModel = DataFilterViewModel;
            ViewBag.ImmovableAssetTypes = await _immovableAssetTypeService.GetAllViewModelWithIncludes();
            ViewBag.AssetTypes = await _immovableAssetTypeService.GetAllViewModelWithIncludes();
            if (vm.MinPrice > 0 && (vm.MaxPrice == null || vm.MaxPrice == 0))
            {
                vm.MaxPrice = DataFilterViewModel.MaxPrice;
            }
            var model = await _favoriteImmovableService.GetAllFavoritesWithFilters(vm, _loggedUser.Id);
            return View(model);
        }
    }
}

