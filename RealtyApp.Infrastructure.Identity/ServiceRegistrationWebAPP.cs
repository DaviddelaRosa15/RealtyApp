using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RealtyApp.Core.Application.Dtos.Account;
using RealtyApp.Core.Application.Interfaces.Repositories;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Domain.Settings;
using RealtyApp.Infrastructure.Identity.Entities;
using RealtyApp.Infrastructure.Identity.Services;
using RealtyApp.Infrastructure.Persistence.Contexts;
using System;
using System.Text;

namespace RealtyApp.Infrastructure.Identity
{
    //Extension Method - Decorator
    public static class ServiceRegistrationWebAPP
    {
        public static void AddIdentityInfrastructureWebAPP(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts

                if (configuration.GetValue<bool>("UseInMemoryDatabase"))
                {
                    services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdentityDb"));
                }
                else
                {
                    services.AddDbContext<IdentityContext>(options =>
                    {
                        options.EnableSensitiveDataLogging();
                        options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                        m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
                    });
                }

            #endregion

            #region Identity

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User";
                options.AccessDeniedPath = "/User/AccessDenied";
            });

            
            services.AddAuthentication();

            #endregion

            #region Services
                services.AddTransient<IAccountService, AccountService>();
            #endregion

        }
    }
}
