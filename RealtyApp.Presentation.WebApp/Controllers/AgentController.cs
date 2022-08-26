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
using System.Linq;
using System.Collections.Generic;

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
        private readonly IImprovementService _improvementService;
        private readonly IUserService _userService;
        public AgentController(IHttpContextAccessor contextAccessor, IImmovableAssetService immovableAssetService, 
                              IImmovableAssetTypeService immovableAssetTypeService, ISellTypeService sellTypeService, 
                              IImprovementService improvementService, IUserService userService)
        {
            _contextAccessor = contextAccessor;
            _loggedUser = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _immovableAssetService = immovableAssetService;
            _immovableAssetTypeService = immovableAssetTypeService;
            _sellTypeService = sellTypeService;
            _improvementService = improvementService;
            _userService = userService;
        }

        #region INDEX
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Index(FilterViewModel vm, string id = null)
        {
            ViewBag.DataFilterViewModel = await _immovableAssetService.GetDataFilterViewModel();
            ViewBag.ImmovableAssetTypes = await _immovableAssetTypeService.GetAllViewModelWithIncludes();
            ViewBag.AssetTypes = await _immovableAssetTypeService.GetAllViewModelWithIncludes();
            
            var model = await _immovableAssetService.GetAllViewModelWithFilters(_loggedUser.Id, vm);
            return View(model);
        }
        #endregion

        #region Display My Immovables
        public async Task<IActionResult> MyImmovables(FilterViewModel vm, string id = null)
        {
            ViewBag.DataFilterViewModel = await _immovableAssetService.GetDataFilterViewModel();
            ViewBag.ImmovableAssetTypes = await _immovableAssetTypeService.GetAllViewModelWithIncludes();
            ViewBag.AssetTypes = await _immovableAssetTypeService.GetAllViewModelWithIncludes();

            var model = await _immovableAssetService.GetAllViewModelWithFilters(_loggedUser.Id, vm);
            return View(model);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(int id)
        {

            if (id == 0)
                return View("404");

            var result = await _immovableAssetService.GetByIdSaveViewModel(id);
            
            if(result == null)
                return NotFound();
            
            return View("ConfirmDelete", result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImmovable(int id)
        {
            var result = await _immovableAssetService.GetByIdSaveViewModel(id);

            if (result == null)
                return NotFound();

            if (result.AgentId != _loggedUser.Id)
                return BadRequest();

            await _immovableAssetService.Delete(result.Id);
            ImageUpload.DeleteFile(id, "Images/ImmovableAssets");

            return RedirectToRoute(new { controller = "Agent", action = "MyImmovables" });
        }
        #endregion

        #region Update and Create

        [HttpGet]
        public async Task<IActionResult> AddImmovable(int id = 0)
        {
            ViewBag.ImmovableAssetTypes = await _immovableAssetTypeService.GetAllViewModel();
            ViewBag.ImmovableSellTypes = await _sellTypeService.GetAllViewModel();
            ViewBag.Improvements = await _improvementService.GetAllViewModel();

            //For creating
            if(id == 0) { 
                return View("FormImmovable", new SaveImmovableAssetViewModel());
            }

            //For updating
            var result = await _immovableAssetService.GetByIdSaveViewModel(id);

            if (result == null)
                return View("404");

            return View(viewName: "FormImmovable", model: result);

        }


        [HttpPost]
        public async Task<IActionResult> AddImmovable(SaveImmovableAssetViewModel saveImmovableAsset)
        {
            ViewBag.ImmovableAssetTypes = await _immovableAssetTypeService.GetAllViewModel();
            ViewBag.ImmovableSellTypes = await _sellTypeService.GetAllViewModel();
            ViewBag.Improvements = await _improvementService.GetAllViewModel();

            saveImmovableAsset.AgentId = _loggedUser.Id;

            if (!ModelState.IsValid)
                return View("FormImmovable", saveImmovableAsset);
            
            if(saveImmovableAsset.FileImg01 == null)
            {
                saveImmovableAsset.HasError = true;
                saveImmovableAsset.ErrorMessage = "Debe de insertar al menos una imagen.";
                return View("FormImmovable", saveImmovableAsset);
            }

            if(saveImmovableAsset.Improvements == null)
            {
                saveImmovableAsset.HasError = true;
                saveImmovableAsset.ErrorMessage = "Debe de seleccionar al menos una mejora para su hogar.";
                return View("FormImmovable", saveImmovableAsset);
            }

            var savedImmovable = await _immovableAssetService.Add(saveImmovableAsset);

            //Persist the images.

            IFormFile[] formFiles = new[] { saveImmovableAsset.FileImg01, saveImmovableAsset.FileImg02, saveImmovableAsset.FileImg03, savedImmovable.FileImg04 };

            var imagesURLs = await ImageUpload.FileUpload(formFiles, savedImmovable.Id, "Images/ImmovableAssets");
            savedImmovable.UrlImage01 = imagesURLs[0];
            savedImmovable.UrlImage02 = imagesURLs[1];
            savedImmovable.UrlImage03 = imagesURLs[2];
            savedImmovable.UrlImage04 = imagesURLs[3];

            await _immovableAssetService.Update(savedImmovable, savedImmovable.Id);

            return RedirectToRoute(new { controller = "Agent", action = "MyImmovables" });

        }


        [HttpPost]
        public async Task<IActionResult> UpdateImmovable(SaveImmovableAssetViewModel saveImmovableAsset)
        {
            ViewBag.ImmovableAssetTypes = await _immovableAssetTypeService.GetAllViewModel();
            ViewBag.ImmovableSellTypes = await _sellTypeService.GetAllViewModel();
            ViewBag.Improvements = await _improvementService.GetAllViewModel();

            if (!ModelState.IsValid)
                return View("FormImmovable", saveImmovableAsset);

            if (saveImmovableAsset.AgentId != _loggedUser.Id)
                return RedirectToRoute(new { controller = "Agent", action = "MyImmovables" }); //Access Denied!

            if (saveImmovableAsset.Improvements == null)
            {
                saveImmovableAsset.HasError = true;
                saveImmovableAsset.ErrorMessage = "Debe de seleccionar al menos una mejora para su hogar.";
                return View("FormImmovable", saveImmovableAsset);
            }

            List<string> currentImages = new() { saveImmovableAsset.UrlImage01, saveImmovableAsset.UrlImage02, saveImmovableAsset.UrlImage03, saveImmovableAsset.UrlImage04 };
            IFormFile[] formFiles = new[] { saveImmovableAsset.FileImg01, saveImmovableAsset.FileImg02, saveImmovableAsset.FileImg03, saveImmovableAsset.FileImg04 };

            List<string> updatedImages = await ImageUpload.FileUpload(formFiles, saveImmovableAsset.Id, true, "Images/ImmovableAssets", currentImages);
            saveImmovableAsset.UrlImage01 = updatedImages[0];
            saveImmovableAsset.UrlImage02 = updatedImages[1];
            saveImmovableAsset.UrlImage03 = updatedImages[2];
            saveImmovableAsset.UrlImage04 = updatedImages[3];


            await _immovableAssetService.Update(saveImmovableAsset, saveImmovableAsset.Id);

            return RedirectToRoute(new { controller = "Agent", action = "MyImmovables" });
        }
        #endregion

        #region MyProfile

        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            return View(viewName: "MyProfile", model: await _userService.GetUserById(_loggedUser.Id));
        }

        [HttpPost]
        public async Task<IActionResult> MyProfile(SaveUserViewModel saveUserView)
        {

            saveUserView.Id = _loggedUser.Id;

            if (ModelState["LastName"].Errors.Count() > 0 || ModelState["FirstName"].Errors.Count() > 0 
                || ModelState["Phone"].Errors.Count() > 0)
            {
                return View(viewName: "MyProfile", model: saveUserView);
            }

            if(saveUserView.File != null)
            {
                saveUserView.ImageUrl = await ImageUpload.FileUpload(saveUserView.File, saveUserView.ImageUrl, saveUserView.Id, "User/Agent", true);
            }

            await _userService.UpdateAgentAsync(saveUserView, saveUserView.Id);

            return RedirectToRoute(new { controller = "Agent", action = "Index" });

        }


        #endregion

    }
}

