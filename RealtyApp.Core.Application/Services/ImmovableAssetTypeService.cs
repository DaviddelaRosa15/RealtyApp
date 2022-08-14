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
        public ImmovableAssetTypeService(IImmovableAssetTypeRepository repository, IMapper mapper) : base(repository, mapper)
        {
            this._mapper = mapper;
            this._typeRepository = repository;
        }

        public async Task<List<ImmovableAssetTypeViewModel>> GetAllViewModelWithIncludes()
        {

            var immovableAssetTypes = await _typeRepository.GetAllWithIncludeAsync(new List<string>() { "ImmovableAssets" });

            List<ImmovableAssetTypeViewModel> immovableAssetTypesViewModels = 
                                                                    _mapper.Map<List<ImmovableAssetTypeViewModel>>(immovableAssetTypes);

            return immovableAssetTypesViewModels;
        }

    }
}
