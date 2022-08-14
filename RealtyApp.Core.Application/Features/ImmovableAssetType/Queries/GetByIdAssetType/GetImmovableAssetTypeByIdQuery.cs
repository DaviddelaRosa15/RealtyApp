using AutoMapper;
using MediatR;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.ImmovableAssetType;
using RealtyApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Features.ImmovableAssetType.Queries.GetByIdAssetType
{
    //<summary>
    // Permite obtener el tipo de inmueble deseado mediante el ID.
    //</summary>


    public class GetImmovableAssetTypeByIdQuery : IRequest<ImmovableAssetTypeDTO>
    {
        [SwaggerParameter(Description = "El id del inmueble que desea obtener.")]
        public int Id { get; set; }
    }

    public class GetImmovableAssetTypeByIdHandler : IRequestHandler<GetImmovableAssetTypeByIdQuery, ImmovableAssetTypeDTO>
    {
        private readonly IImmovableAssetTypeRepository _assetTypeRepository;
        private readonly IMapper _maper;

        public GetImmovableAssetTypeByIdHandler(IImmovableAssetTypeRepository assetTypeRepository, IMapper maper)
        {
            _assetTypeRepository = assetTypeRepository;
            _maper = maper;
        }

        public async Task<ImmovableAssetTypeDTO> Handle(GetImmovableAssetTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await GetImmovableAssetTypeDTOById(request.Id);
            
            if (result == null)
                 throw new Exception($"Immovable Asset type not found.");
            else
                return result;
        }

        private async Task<ImmovableAssetTypeDTO> GetImmovableAssetTypeDTOById(int id)
        {
            var result = await _assetTypeRepository.GetByIdAsync(id);
            
            if (result == null)
                return null;
            else
                return _maper.Map<ImmovableAssetTypeDTO>(result);
        }


    }


}
