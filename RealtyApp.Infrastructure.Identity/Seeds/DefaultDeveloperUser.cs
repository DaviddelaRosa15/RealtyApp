using Microsoft.AspNetCore.Identity;
using RealtyApp.Core.Application.Enums;
using RealtyApp.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Infrastructure.Identity.Seeds
{
    public static class DefaultDeveloperUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new();
            defaultUser.UserName = "developeruser";
            defaultUser.Email = "developeruser@email.com";
            defaultUser.FirstName = "Edwin";
            defaultUser.LastName = "Roman";
            defaultUser.CardIdentification = "00112304123";
            defaultUser.PhoneNumber = "8299421244";
            //imagen por defecto
            defaultUser.UrlImage = "";
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;

            if(userManager.Users.All(u=> u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Developer.ToString());
                }
            }
         
        }
    }
}
