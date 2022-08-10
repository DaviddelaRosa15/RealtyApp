using Microsoft.AspNetCore.Mvc;
using RealtyApp.Core.Application.Interfaces.Services;
using System.IO;
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
