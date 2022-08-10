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

namespace RealtyApp.Presentation.WebApp.Controllers
{
    public class AgentController : Controller
    {
        private readonly IUserService _userService;
        public AgentController(IUserService userService)
        {
            _userService = userService;
        }      
        

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Register()
        {
            return View(new SaveUserViewModel());
        }

    
        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if(vm.TypeUser== "Agent")
            {
                RegisterResponse response = await _userService.RegisterAgentUserAsync(vm);
                if (response.HasError)
                {
                    vm.HasError = response.HasError;
                    vm.Error = response.Error;
                    return View(vm);
                }
                vm.ImageUrl = UploadFile(vm.File, vm.TypeUser,response.Id);
                await _userService.UpdateAsync(response.Id);
            }
            else
            {
                var origin = Request.Headers["origin"];
                RegisterResponse response = await _userService.RegisterClientUserAsync(vm, origin);
                if (response.HasError)
                {
                    vm.HasError = response.HasError;
                    vm.Error = response.Error;
                    return View(vm);
                }
                vm.ImageUrl = UploadFile(vm.File, vm.TypeUser, response.Id);
                await _userService.UpdateAsync(response.Id);
            }
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> DeleteAgent(string id)
        {
            await _userService.DeleteAsync(id);
            string basePath = $"/Images/Agent/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }
            return RedirectToRoute(new { controller = "Administrator", action = "Index" });
        }


        #region SaveImage
        private string UploadFile(IFormFile file, string rol,string id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/{rol}/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }
        #endregion


    }
}

