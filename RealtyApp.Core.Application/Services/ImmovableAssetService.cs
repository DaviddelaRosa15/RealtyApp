using AutoMapper;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.ImmovableAsset;
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
        private readonly IUserService _userService;
        private readonly IImprovementRepository _improvementRepository;
        private readonly IImprovement_ImmovableRepository _improvement_ImmovableRepository;

        public ImmovableAssetService(IImmovableAssetRepository ImmovableAssetRepository, 
            IMapper mapper, IUserService userService,
            IImprovementRepository improvementRepository,
            IImprovement_ImmovableRepository improvement_ImmovableRepository)
        : base(ImmovableAssetRepository, mapper)
        {
            _immovableAssetRepository = ImmovableAssetRepository;
            _mapper = mapper;
            _userService = userService;
            _improvementRepository = improvementRepository;
            _improvement_ImmovableRepository = improvement_ImmovableRepository;
        }

        public async Task<List<ImmovableAssetViewModel>> GetAllViewModelWithFilters(FilterViewModel filters, string id)
        {
            List<ImmovableAssetViewModel> filterListViewModels = new();
            List<ImmovableAssetViewModel> listViewModels = new();
            var assetList = await _immovableAssetRepository.GetAllWithIncludeAsync(new List<string> { "ImmovableAssetType", "SellType" });

            if (id != null)
            {
                listViewModels = assetList.Where(x => x.AgentId == id).Select(asset => new ImmovableAssetViewModel
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
                    AgentId = asset.AgentId,
                    ImmovableAssetTypeId = asset.ImmovableAssetType.Id,
                    ImmovableAssetTypeName = asset.ImmovableAssetType.Name,
                    SellTypeId = asset.SellType.Id,
                    SellTypeName = asset.SellType.Name
                }).ToList();
            }
            else
            {
                listViewModels = assetList.Select(asset => new ImmovableAssetViewModel
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
                    AgentId = asset.AgentId,
                    ImmovableAssetTypeId = asset.ImmovableAssetType.Id,
                    ImmovableAssetTypeName = asset.ImmovableAssetType.Name,
                    SellTypeId = asset.SellType.Id,
                    SellTypeName = asset.SellType.Name
                }).ToList();
            }

            #region Aplicar filtros
            if (filters.ImmovableAssetTypeId != null && filters.ImmovableAssetTypeId != 0)
            {
                filterListViewModels = listViewModels.Where(x => x.ImmovableAssetTypeId == filters.ImmovableAssetTypeId).ToList();
            }
            if (filters.Code != null)
            {
                filterListViewModels = filterListViewModels.Where(x => x.Code == filters.Code).ToList();
            }
            if (filters.MaxPrice != 0)
            {
                filterListViewModels = filterListViewModels.Where(x => x.Price >= filters.MinPrice && x.Price <= filters.MaxPrice).ToList();
            }
            if (filters.BedroomQuantity != 0)
            {
                filterListViewModels = filterListViewModels.Where(x => x.BedroomQuantity == filters.BedroomQuantity).ToList();
            }
            if (filters.BathroomQuantity != 0)
            {
                filterListViewModels = filterListViewModels.Where(x => x.BathroomQuantity == filters.BathroomQuantity).ToList();
            }

            #endregion

            return filterListViewModels == null || filterListViewModels.Count == 0 ? listViewModels : filterListViewModels;
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
                AgentId = asset.AgentId,
                SellTypeId = asset.SellType.Id,
                SellTypeName = asset.SellType.Name
            }).ToList();
        }

        public async Task<DetailsViewModel> GetDetailsViewModel(int id)
        {
            var assetList = await _immovableAssetRepository.GetAllWithIncludeAsync(new List<string> { "ImmovableAssetType", "SellType", "Improvement_Immovables" });

            return assetList.Select(asset => new DetailsViewModel
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
                AgentName = _userService.GetUserById(asset.AgentId).Result.FirstName + " " +
                _userService.GetUserById(asset.AgentId).Result.LastName,
                AgentImgUrl = _userService.GetUserById(asset.AgentId).Result.ImageUrl,
                AgentPhone = _userService.GetUserById(asset.AgentId).Result.Phone,
                AgentEmail = _userService.GetUserById(asset.AgentId).Result.Email,
                ImmovableAssetTypeName = asset.ImmovableAssetType.Name,
                SellTypeName = asset.SellType.Name,
                ImprovementNames = asset.Improvement_Immovables.Select(x => x.Improvement.Name).ToList()
            }).FirstOrDefault();
        }
        public async Task<List<DetailsViewModelApi>> GetIncludeDetails()        {
            var assetList = await _immovableAssetRepository.GetAllWithIncludeAsync(new List<string> { "ImmovableAssetType", "SellType", "Improvement_Immovables"});
            var improvementList = await _improvement_ImmovableRepository.GetAllWithIncludeAsync(new List<string> { "Improvement" });

            return assetList.Select(asset => new DetailsViewModelApi
            {
                Id = asset.Id,
                Code = asset.Code,
                Address = asset.Address,
                Description = asset.Description,
                Price = asset.Price,
                Meters = asset.Meters,
                BedroomQuantity = asset.BedroomQuantity,
                BathroomQuantity = asset.BathroomQuantity,
                AgentName = _userService.GetUserById(asset.AgentId).Result.FirstName,
                AgentId = asset.AgentId,
                ImmovableAssetTypeName = asset.ImmovableAssetType.Name,
                SellTypeName = asset.SellType.Name,
                ImprovementNames = asset.Improvement_Immovables.Select(x => x.Improvement.Name).ToList()
                //improvementList.Where(x => x.ImmovableAssetId==asset.Id)
                //.Select(x=>x.Improvement.Name).ToList()

            }).ToList();
        }
        public async Task<DetailsViewModelApi> GetIncludeDetailsById(int id)
        {
            var assetList = await _immovableAssetRepository.GetAllWithIncludeAsync(new List<string> { "ImmovableAssetType", "SellType", "Improvement_Immovables" });
            var improvementList = await _improvement_ImmovableRepository.GetAllWithIncludeAsync(new List<string> { "Improvement" });
            var assetFilter = assetList.Where(x => x.Id == id).FirstOrDefault();

            DetailsViewModelApi viewModelApi = new();
            if (assetFilter != null)
            {
                viewModelApi.Id = assetFilter.Id;
                viewModelApi.Code = assetFilter.Code;
                viewModelApi.Address = assetFilter.Address;
                viewModelApi.Description = assetFilter.Description;
                viewModelApi.Price = assetFilter.Price;
                viewModelApi.Meters = assetFilter.Meters;
                viewModelApi.BedroomQuantity = assetFilter.BedroomQuantity;
                viewModelApi.BathroomQuantity = assetFilter.BathroomQuantity;
                viewModelApi.AgentName = _userService.GetUserById(assetFilter.AgentId).Result.FirstName;
                viewModelApi.AgentId =assetFilter.AgentId;
                viewModelApi.ImmovableAssetTypeName = assetFilter.ImmovableAssetType.Name;
                viewModelApi.SellTypeName = assetFilter.SellType.Name;
                viewModelApi.ImprovementNames = assetFilter.Improvement_Immovables.Select(x => x.Improvement.Name).ToList();
                    //improvementList.Where(x => x.ImmovableAssetId == assetFilter.Id)
                    //.Select(x => x.Improvement.Name).ToList();
                return viewModelApi;
            }
            else
            {
                return null;
            }
        }
        public async Task<DetailsViewModelApi> GetIncludeDetailsByCode(string code)
        {
            var assetList = await _immovableAssetRepository.GetAllWithIncludeAsync(new List<string> { "ImmovableAssetType", "SellType", "Improvement_Immovables" });
            var improvementList = await _improvement_ImmovableRepository.GetAllWithIncludeAsync(new List<string> { "Improvement" });
            var assetFilter = assetList.Where(x => x.Code == code).FirstOrDefault();

            DetailsViewModelApi viewModelApi = new();
            if (assetFilter != null)
            {
                viewModelApi.Id = assetFilter.Id;
                viewModelApi.Code = assetFilter.Code;
                viewModelApi.Address = assetFilter.Address;
                viewModelApi.Description = assetFilter.Description;
                viewModelApi.Price = assetFilter.Price;
                viewModelApi.Meters = assetFilter.Meters;
                viewModelApi.BedroomQuantity = assetFilter.BedroomQuantity;
                viewModelApi.BathroomQuantity = assetFilter.BathroomQuantity;
                viewModelApi.AgentName = _userService.GetUserById(assetFilter.AgentId).Result.FirstName;
                viewModelApi.AgentId =assetFilter.AgentId;
                viewModelApi.ImmovableAssetTypeName = assetFilter.ImmovableAssetType.Name;
                viewModelApi.SellTypeName = assetFilter.SellType.Name;
                viewModelApi.ImprovementNames = assetFilter.Improvement_Immovables.Select(x => x.Improvement.Name).ToList();
                    //improvementList.Where(x => x.ImmovableAssetId == assetFilter.Id)
                    //.Select(x => x.Improvement.Name).ToList();
                return viewModelApi;
            }
            else
            {
                return null;
            }
           

        }

        public async Task<int> CountImmovobleAsset()
        {
            return await _immovableAssetRepository.CountImmovobleAsset();
        }

        public async Task<int> CountImmovablesByAgent(string id)
        {
            var immovables = await GetAllViewModelWithIncludes();
            int count = immovables.Where(x => x.AgentId == id).Count();
            return count;
        }

        public async Task<DataFilterViewModel> GetDataFilterViewModel()
        {
            var assetList = await _immovableAssetRepository.GetAllWithIncludeAsync(new List<string> { "ImmovableAssetType", "SellType" });

            return assetList.Select(asset => new DataFilterViewModel
            {
                MinPrice = assetList.Max(x => x.Price),
                MaxPrice = assetList.Min(x => x.Price),
                MaxBedroomQuantity = assetList.Max(x => x.BedroomQuantity),
                MaxBathroomQuantity = assetList.Max(x => x.BathroomQuantity)
            }).FirstOrDefault();
        }

        public async Task DeleteByIdAgent(string id)
        {
            await _immovableAssetRepository.DeleteByIdAgent(id);
        }

        public async Task<int> CountImmovableTypeById(int id)
        {
            return await _immovableAssetRepository.CountImmovableTypeById(id);
        }
        public async Task<int> CountSellTypeById(int id)
        {
            return await _immovableAssetRepository.CountSellTypeById(id);
        }
    }
}
