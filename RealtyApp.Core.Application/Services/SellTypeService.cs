using AutoMapper;
using RealtyApp.Core.Application.Interfaces.Repositories;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Application.ViewModels.SellType;
using RealtyApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Services
{
    public class SellTypeService : GenericService<SaveSellTypeViewModel, SellTypeViewModel, SellType>, ISellTypeService
    {
        private readonly IMapper _mapper;
        private readonly ISellTypeRepository _repository;
        private readonly IImmovableAssetRepository _ImmovableService;
        public SellTypeService(ISellTypeRepository repository, 
            IMapper mapper,
             IImmovableAssetRepository ImmovableService) : base(repository, mapper)
        {
            this._mapper = mapper;
            this._repository = repository;
            this._ImmovableService = ImmovableService;
        }

        public async Task<List<SellTypeViewModel>> GetAllViewModelWithIncludes()
        {
            var sellTypes = await _repository.GetAllWithIncludeAsync(new List<string>() { "ImmovableAssets" });

            List<SellTypeViewModel> sellTypesViews = _mapper.Map<List<SellTypeViewModel>>(sellTypes);

            return sellTypesViews;
        }
        public async Task<List<SellTypeViewModel>> GetAllWithSellType()
        {
            List<SellTypeViewModel> sellTypes = new();
            var immovableTypes = await _repository.GetAllAsync();
            foreach (var sellType in immovableTypes)
            {
                sellTypes.Add(new SellTypeViewModel()
                {
                    Id = sellType.Id,
                    Name = sellType.Name,
                    Description = sellType.Description,
                    CountUseType = await _ImmovableService.CountSellTypeById(sellType.Id)

                }); ;
            }
            return sellTypes;
        }


    }
}
