using Microsoft.AspNetCore.Mvc;
using RealtyApp.Core.Application.Dtos.Account;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Application.ViewModels.User;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyApp.Presentation.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
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
            else
            {
                RegisterResponse response = await _userService.RegisterAdministratorUser(vm);
                if (response.HasError)
                {
                    vm.HasError = response.HasError;
                    vm.Error = response.Error;
                    return View(vm);
                }
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
                return RedirectToRoute(new { Controller = "Admin", Action = "Admins" });
            }
            return RedirectToRoute(new {Controller = "Admin", Action = "Developers"});
        }

        public async Task<IActionResult> Developers()
        {
            return View(await _userService.GetAllUsersDeveloper());
        }

        public async Task<IActionResult> ChangeConfirmDevelopers(string id)
        {
            await _userService.ChangeUserStatus(id);
            return RedirectToRoute(new { controller = "Admin", action = "Developers" });
        }

        public async Task<IActionResult> DeleteAgent(string id)
        {
            await _userService.DeleteAsync(id);
            string basePath = $"/Images/User/Agent/{id}";
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
    }
}
