using AutoMapper;
using RealtyApp.Core.Application.Interfaces.Repositories;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Application.ViewModels.FavoriteImmovable;
using RealtyApp.Core.Application.ViewModels.ImmovableAsset;
using RealtyApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Services
{
    public class FavoriteImmovableService : GenericService<SaveFavoriteImmovableViewModel, FavoriteImmovableViewModel, FavoriteImmovable>, IFavoriteImmovableService
    {
        private readonly IFavoriteImmovableRepository _favoriteImmRepository;
        private readonly IMapper _mapper;

        public FavoriteImmovableService(IFavoriteImmovableRepository favoriteImmRepository, IMapper mapper)
        : base(favoriteImmRepository, mapper)
        {
            _favoriteImmRepository = favoriteImmRepository;
            _mapper = mapper;
        }

        public async Task<List<FavoriteImmovableAssetViewModel>> GetAllFavoritesWithFilters(FilterViewModel filters, string idClient)
        {
            List<FavoriteImmovableAssetViewModel> filterListViewModels = new();
            var favList = await _favoriteImmRepository.GetAllWithIncludeAsync(new List<string> { "ImmovableAsset" });

            var listViewModels = favList.OrderByDescending(x => x.Created).Where(x => x.ClientId == idClient).Select(fav => new FavoriteImmovableAssetViewModel
            {
                Id = fav.ImmovableAsset.Id,
                Code = fav.ImmovableAsset.Code,
                Description = fav.ImmovableAsset.Description,
                Address = fav.ImmovableAsset.Address,
                Price = fav.ImmovableAsset.Price,
                UrlImage01 = fav.ImmovableAsset.UrlImage01,
                UrlImage02 = fav.ImmovableAsset.UrlImage02,
                UrlImage03 = fav.ImmovableAsset.UrlImage03,
                UrlImage04 = fav.ImmovableAsset.UrlImage04,
                Meters = fav.ImmovableAsset.Meters,
                BedroomQuantity = fav.ImmovableAsset.BedroomQuantity,
                BathroomQuantity = fav.ImmovableAsset.BathroomQuantity,
                AgentId = fav.ImmovableAsset.AgentId,
                ImmovableAssetTypeId = fav.ImmovableAsset.ImmovableAssetTypeId,
                ImmovableAssetTypeName = fav.ImmovableAsset.ImmovableAssetType.Name,
                SellTypeName = fav.ImmovableAsset.SellType.Name
            }).ToList();

            if (filters.ImmovableAssetTypeId == null && filters.Code == null && filters.MinPrice == null &&
                filters.MaxPrice == null && filters.BedroomQuantity == null && filters.BathroomQuantity == null)
            {
                return listViewModels;
            }

            #region Aplicar filtros

            if (filters.MaxPrice != null && filters.MaxPrice != 0)
            {
                filterListViewModels = listViewModels.Where(x => x.Price >= filters.MinPrice && x.Price <= filters.MaxPrice).ToList();
            }
            if (filters.ImmovableAssetTypeId != null && filters.ImmovableAssetTypeId != 0)
            {
                if (filterListViewModels.Count == 0)
                {
                    filterListViewModels = listViewModels.Where(x => x.ImmovableAssetTypeId == filters.ImmovableAssetTypeId).ToList();
                }
                else
                {
                    filterListViewModels = filterListViewModels.Where(x => x.ImmovableAssetTypeId == filters.ImmovableAssetTypeId).ToList();
                }
            }
            if (filters.Code != null)
            {
                if (filterListViewModels.Count == 0 && (filters.ImmovableAssetTypeId == null || filters.ImmovableAssetTypeId == 0))
                {
                    filterListViewModels = listViewModels.Where(x => x.Code == filters.Code).ToList();
                }
                else
                {
                    filterListViewModels = filterListViewModels.Where(x => x.Code == filters.Code).ToList();
                }
            }

            if (filters.BedroomQuantity != null && filters.BedroomQuantity != 0)
            {
                if (filterListViewModels.Count == 0 && (filters.ImmovableAssetTypeId == null || filters.ImmovableAssetTypeId == 0) && filters.Code == null)
                {
                    filterListViewModels = listViewModels.Where(x => x.BedroomQuantity == filters.BedroomQuantity).ToList();
                }
                else
                {
                    filterListViewModels = filterListViewModels.Where(x => x.BedroomQuantity == filters.BedroomQuantity).ToList();
                }

            }
            if (filters.BathroomQuantity != null && filters.BathroomQuantity != 0)
            {
                if (filterListViewModels.Count == 0 && (filters.ImmovableAssetTypeId == null || filters.ImmovableAssetTypeId == 0)
                    && filters.Code == null && (filters.BedroomQuantity == null || filters.BedroomQuantity == 0))
                {
                    filterListViewModels = listViewModels.Where(x => x.BathroomQuantity == filters.BathroomQuantity).ToList();
                }
                else
                {
                    filterListViewModels = filterListViewModels.Where(x => x.BathroomQuantity == filters.BathroomQuantity).ToList();
                }

            }

            return filterListViewModels;

            #endregion

        }

        public async Task ManageFavoriteImmovable(SaveFavoriteImmovableViewModel vm)
        {
            var stock = await GetAllViewModel();

            if (stock.Any(x => x.ImmovableAssetId == vm.ImmovableAssetId && x.ClientId == vm.ClientId))
            {
                foreach (var item in stock)
                {
                    if (item.ImmovableAssetId == vm.ImmovableAssetId && item.ClientId == vm.ClientId)
                    {
                        await Delete(item.Id);
                    }
                }
            }
            else
            {
                await Add(vm);
            }
        }

        public async Task<bool> IsFavoriteImmovable(int id, string idClient)
        {
            var stock = await GetAllViewModel();

            if (stock.Any(x => x.ImmovableAssetId == id && x.ClientId == idClient))
            {
                return true;
            }

            return false;
        }
    }
}
