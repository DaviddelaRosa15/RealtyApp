using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealtyApp.Core.Application.Dtos.Account;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Application.ViewModels.User;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyApp.Presentation.WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IImmovableAssetService _immovableAssetService;
        public AdminController(IUserService userService, IImmovableAssetService immovableAssetService)
        {
            _userService = userService;
            _immovableAssetService = immovableAssetService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.countImmovable = await _immovableAssetService.CountImmovobleAsset();
            ViewBag.client = await _userService.CountClient();
            ViewBag.agent = await _userService.CountAgent();
            ViewBag.developer = await _userService.CountDeveloper();
            return View();
        }

        public async Task<IActionResult> Developers()
        {
            return View(await _userService.GetAllUsersDeveloper());
        }

        public async Task<IActionResult> Administrators()
        {
            return View(await _userService.GetAllUsersAdmin());
        }

        public async Task<IActionResult> Agents()
        {
            return View(await _userService.GetAllUserAgentAsync());
        }

        public async Task<IActionResult> SwithUserStatus(string id, string type)
        {
            var operationStatus = await _userService.ChangeUserStatus(id);

            if (operationStatus)
                return RedirectToRoute(new { controller = "Admin", action = type });
            else
                return StatusCode(400);
        }

        public IActionResult Register(string type)
        {
            SaveUserViewModel model = new()
            {
                TypeUser = type
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (vm.TypeUser == "Developer")
            {
                RegisterResponse response = await _userService.RegisterDeveloperUser(vm);
                if (response.HasError)
                {
                    vm.HasError = response.HasError;
                    vm.Error = response.Error;
                    return View(vm);
                }
                return RedirectToRoute(new { controller = "Admin", action = "Developers" });
            }
            if(vm.TypeUser == "Administrator")
            {
                RegisterResponse response = await _userService.RegisterAdministratorUser(vm);
                if (response.HasError)
                {
                    vm.HasError = response.HasError;
                    vm.Error = response.Error;
                    return View(vm);
                }

                return RedirectToRoute(new { controller = "Admin", action = "Administrators" });
            }

            return RedirectToRoute(new { controller = "Admin", action = "Index" });

        }


        public async Task<IActionResult> Edit(string id, string type)
        {
            var model = await _userService.GetUserById(id);
            model.TypeUser = type;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveUserViewModel vm)
        {
            if (ModelState["Email"].Errors.Any() || ModelState["FirstName"].Errors.Any()
                || ModelState["LastName"].Errors.Any() || ModelState["CardIdentification"].Errors.Any()
                || ModelState["Username"].Errors.Any() || ModelState["Phone"].Errors.Any())
            {
                return View(vm);
            }

            var response = await _userService.Update(vm, vm.Id);

            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }

            if (vm.TypeUser == "Administrator")
            {
                return RedirectToRoute(new { Controller = "Admin", Action = "Administrators" });
            }
            return RedirectToRoute(new {Controller = "Admin", Action = "Developers"});
        }

        public async Task<IActionResult> Delete(string id, string type)
        {
           SaveUserViewModel user =await _userService.GetUserById(id);
            user.TypeUser = type;
            return View(user);
        }
        public async Task<IActionResult> DeleteUser(string id, string type)
        {
            await _userService.DeleteAsync(id);
            if (type == "Agent")
            {
                await _immovableAssetService.DeleteByIdAgent(id);
            }
            string basePath = $"/Images/User/{type}/{id}";
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
            return RedirectToRoute(new { controller = "Admin", action = $"{type}s"});
        }
    }
}
