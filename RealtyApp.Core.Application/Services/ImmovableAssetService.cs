using AutoMapper;
using RealtyApp.Core.Application.Interfaces.Repositories;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Application.ViewModels.ImmovableAsset;
using RealtyApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Services
{
   public class ImmovableAssetService : GenericService<SaveImmovableAssetViewModel, ImmovableAssetViewModel, ImmovableAsset>, IImmovableAssetService
    {
        private readonly IImmovableAssetRepository _immovableAssetRepository;
        private readonly IMapper _mapper;

        public ImmovableAssetService(IImmovableAssetRepository ImmovableAssetRepository, IMapper mapper)
        : base(ImmovableAssetRepository, mapper)
        {
            _immovableAssetRepository = ImmovableAssetRepository;
            _mapper = mapper;
        }

        public async Task<List<ImmovableAssetViewModel>> GetAllViewModelWithFilters(FilterViewModel filters)
        {
            var assetList = await _immovableAssetRepository.GetAllWithIncludeAsync(new List<string> { "ImmovableAssetType", "SellType" });

            var listViewModels = assetList.Select(asset => new ImmovableAssetViewModel
            {
                Id = asset.Id,
                Code = asset.Code,
                Description = asset.Description,                
                Price = asset.Price,
                UrlImage01 = asset.UrlImage01,
                UrlImage02 = asset.UrlImage02,
                UrlImage03 = asset.UrlImage03,
                UrlImage04 = asset.UrlImage04,
                Meters = asset.Meters,
                BedroomQuantity = asset.BedroomQuantity,
                BathroomQuantity = asset.BathroomQuantity,
                ImmovableAssetTypeId = asset.ImmovableAssetType.Id,
                ImmovableAssetTypeName = asset.ImmovableAssetType.Name,
                SellTypeId = asset.SellType.Id,
                SellTypeName = asset.SellType.Name
            }).ToList();

            #region aplicar filtros
            //if (filters.CategoryId != null)
            //{
            //    foreach (int item in filters.CategoryId)
            //    {
            //        var filterListViewModels = articleList.Where(article => article.UserId != userViewModel.Id).Select(article => new ArticleViewModel
            //        {
            //            Name = article.Name,
            //            Description = article.Description,
            //            Id = article.Id,
            //            Price = article.Price,
            //            ImageUrlOne = article.ImageUrlOne,
            //            ImageUrlTwo = article.ImageUrlTwo,
            //            ImageUrlThree = article.ImageUrlThree,
            //            ImageUrlFour = article.ImageUrlFour,
            //            CategoryName = article.Category.Name,
            //            CategoryId = article.Category.Id
            //        }).ToList();

            //        filterListViewModels = filterListViewModels.Where(article => article.CategoryId == item).ToList();

            //        foreach (ArticleViewModel article in filterListViewModels)
            //        {
            //            filterList.Add(new ArticleViewModel()
            //            {
            //                Name = article.Name,
            //                Description = article.Description,
            //                Id = article.Id,
            //                Price = article.Price,
            //                ImageUrlOne = article.ImageUrlOne,
            //                ImageUrlTwo = article.ImageUrlTwo,
            //                ImageUrlThree = article.ImageUrlThree,
            //                ImageUrlFour = article.ImageUrlFour,
            //                CategoryName = article.CategoryName,
            //                CategoryId = article.CategoryId
            //            });
            //        }

            //    }
            //    if (filters.CategoryId[0] == 0)
            //    {
            //        listViewModels = listViewModels.OrderBy(article => article.Name).ToList();
            //        return listViewModels.ToList();
            //    }
            //    else
            //    {
            //        filterList = filterList.OrderBy(article => article.Name).ToList();
            //        return filterList.ToList();
            //    }
            //}
            //else if (filters.ArticleName != null)
            //{
            //    listViewModels = listViewModels.Where(article => article.Name.Contains(filters.ArticleName)).ToList();
            //}

            //listViewModels = listViewModels.OrderBy(article => article.Name).ToList();
            #endregion

            return listViewModels;
        }

        public async Task<List<ImmovableAssetViewModel>> GetAllViewModelWithIncludes()
        {
            var assetList = await _immovableAssetRepository.GetAllWithIncludeAsync(new List<string> { "ImmovableAssetType", "SellType" });

            return assetList.Select(asset => new ImmovableAssetViewModel
            {
                Id = asset.Id,
                Code = asset.Code,
                Description = asset.Description,
                Price = asset.Price,
                UrlImage01 = asset.UrlImage01,
                UrlImage02 = asset.UrlImage02,
                UrlImage03 = asset.UrlImage03,
                UrlImage04 = asset.UrlImage04,
                Meters = asset.Meters,
                BedroomQuantity = asset.BedroomQuantity,
                BathroomQuantity = asset.BathroomQuantity,
                ImmovableAssetTypeId = asset.ImmovableAssetType.Id,
                ImmovableAssetTypeName = asset.ImmovableAssetType.Name,
                SellTypeId = asset.SellType.Id,
                SellTypeName = asset.SellType.Name
            }).ToList();
        }
    }
}
