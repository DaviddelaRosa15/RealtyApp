using RealtyApp.Core.Application.ViewModels.FavoriteImmovable;
using RealtyApp.Core.Application.ViewModels.ImmovableAsset;
using RealtyApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Interfaces.Services
{
   public interface IFavoriteImmovableService:IGenericService<SaveFavoriteImmovableViewModel, FavoriteImmovableViewModel, FavoriteImmovable>
   {
        Task<List<FavoriteImmovableAssetViewModel>> GetAllFavoritesWithFilters(FilterViewModel filters, string idClient);
        Task ManageFavoriteImmovable(SaveFavoriteImmovableViewModel vm);
        Task<bool> IsFavoriteImmovable(int id, string idClient);
   }
}
