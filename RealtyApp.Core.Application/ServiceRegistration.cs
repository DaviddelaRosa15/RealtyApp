using RealtyApp.Core.Application.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealtyApp.Core.Application.Interfaces.Repositories;
using RealtyApp.Core.Application.Interfaces.Services;
using System.Reflection;

namespace RealtyApp.Infrastructure.Persistence
{

    //Extension Method - Decorator
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
           
            #region Services
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IFavoriteImmovableService, FavoriteImmovableService>();
            services.AddTransient<IImmovableAssetService, ImmovableAssetService>();
            services.AddTransient<IImprovementService, ImprovementService>();
            services.AddTransient<IImmovableAssetTypeService, ImmovableAssetTypeService>();
            #endregion

        }
    }
}
