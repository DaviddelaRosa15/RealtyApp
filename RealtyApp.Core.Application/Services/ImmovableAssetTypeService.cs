using AutoMapper;
using RealtyApp.Core.Application.Interfaces.Repositories;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Application.ViewModels.ImmovableAssetType;
using RealtyApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Services
{
    public class ImmovableAssetTypeService : GenericService<ImmovableAssetTypeSaveViewModel, ImmovableAssetTypeViewModel, ImmovableAssetType>, IImmovableAssetTypeService
    {
        private readonly IMapper _mapper;
        private readonly IImmovableAssetTypeRepository _typeRepository;
        private readonly IImmovableAssetRepository _ImmovableService;
        public ImmovableAssetTypeService(IImmovableAssetTypeRepository repository, 
            IMapper mapper,
            IImmovableAssetRepository ImmovableService) : base(repository, mapper)
        {
            this._mapper = mapper;
            this._typeRepository = repository;
            this._ImmovableService = ImmovableService;
        }

        public override async Task Update(ImmovableAssetTypeSaveViewModel vm, int id)
        {
            var beforeUpdate = await GetByIdSaveViewModel(id);
            vm.Created = beforeUpdate.Created;
            vm.CreatedBy = beforeUpdate.CreatedBy;
            await base.Update(vm, id);
        }

        public async Task<List<ImmovableAssetTypeViewModel>> GetAllViewModelWithIncludes()
        {

            var immovableAssetTypes = await _typeRepository.GetAllWithIncludeAsync(new List<string>() { "ImmovableAssets" });

            List<ImmovableAssetTypeViewModel> immovableAssetTypesViewModels = 
                                                                    _mapper.Map<List<ImmovableAssetTypeViewModel>>(immovableAssetTypes);

            return immovableAssetTypesViewModels;
        }
        public async Task<List<ImmovableAssetTypeViewModel>> GetAllWithCountTypeImmovableUse()
        {
            List<ImmovableAssetTypeViewModel> immovableAssetTypeViewModels = new();
            var immovableTypes= await _typeRepository.GetAllAsync();
            foreach (var immovable in immovableTypes)
            {
                immovableAssetTypeViewModels.Add(new ImmovableAssetTypeViewModel()
                {
                    Id = immovable.Id,
                    Name = immovable.Name,
                    Description = immovable.Description,
                    CountUseType =await _ImmovableService.CountImmovableTypeById(immovable.Id)

                }) ;
            }
            return immovableAssetTypeViewModels;
        }

    }
}
