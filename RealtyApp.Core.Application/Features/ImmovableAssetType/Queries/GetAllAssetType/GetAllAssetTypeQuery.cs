using AutoMapper;
using MediatR;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.ImmovableAssetType;
using RealtyApp.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Features.ImmovableAssetType.Queries.GetAllAssetType
{
    //<summary>
    // Permite obtener todos los tipos de inmuebles registrados en el sistema.
    //</summary>

    public class GetAllAssetTypeQuery : IRequest<IEnumerable<ImmovableAssetTypeDTO>>
    {

    }

    public class GetAllImmovableAssetTypeQueryHandler : IRequestHandler<GetAllAssetTypeQuery, IEnumerable<ImmovableAssetTypeDTO>>
    {

        private readonly IImmovableAssetTypeRepository _assetTypeRepository;
        private readonly IMapper _mapper;

        public GetAllImmovableAssetTypeQueryHandler(IImmovableAssetTypeRepository assetTypeRepository, IMapper maper)
        {
            _assetTypeRepository = assetTypeRepository;
            _mapper = maper;
        }

        public async Task<IEnumerable<ImmovableAssetTypeDTO>> Handle(GetAllAssetTypeQuery request, CancellationToken cancellationToken)
        {
            var result = await GetAllDTOsWithIncludes();

            if (result == null || result.Count == 0)
                return null;
            else
                return result;
        }

        private async Task<List<ImmovableAssetTypeDTO>> GetAllDTOsWithIncludes()
        {

            var immovableAssetTypes = await _assetTypeRepository.GetAllWithIncludeAsync(new List<string>() { "ImmovableAssets" });

            List<ImmovableAssetTypeDTO> immovableAssetTypesViewModels = _mapper.Map<List<ImmovableAssetTypeDTO>>(immovableAssetTypes);

            return immovableAssetTypesViewModels;
        }

    }


}
