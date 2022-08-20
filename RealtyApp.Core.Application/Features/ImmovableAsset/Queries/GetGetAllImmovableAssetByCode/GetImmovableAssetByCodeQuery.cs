using AutoMapper;
using MediatR;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.ImmovableAsset;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Application.ViewModels.ImmovableAsset;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Features.ImmovableAsset.Queries.GetGetAllImmovableAssetByCode
{
    //<summary>
    // Permite obtener una propiedad mediante su codigo.
    //</summary>

    public class GetImmovableAssetByCodeQuery:IRequest<ImmovableAssetDTO>
    {
        [SwaggerParameter(Description = "Introducir el codigo de la propiedad a obtener")]
        public string Code { get; set; }
    }

    public class GetImmovableAssetByCodeQueryHandler : IRequestHandler<GetImmovableAssetByCodeQuery, ImmovableAssetDTO>
    {
        private readonly IImmovableAssetService _immovableAssetService;
        private readonly IMapper _mapper;
        public GetImmovableAssetByCodeQueryHandler(IMapper mapper
            , IImmovableAssetService immovableAssetService)
        {
            _immovableAssetService = immovableAssetService;
            _mapper = mapper;
        }

        public async Task<ImmovableAssetDTO> Handle(GetImmovableAssetByCodeQuery request, CancellationToken cancellationToken)
        {
            var immovableAssets = await GetIncludeDetailsById(request.Code);
            if (immovableAssets != null)
            {
                return immovableAssets;
            }
            return null;
        }
        private async Task<ImmovableAssetDTO> GetIncludeDetailsById(string code)
        {
            DetailsViewModelApi immovableAssetDetails = await _immovableAssetService.GetIncludeDetailsByCode(code);
            ImmovableAssetDTO immovableAssetTypesViewModels = new(); 
            if (immovableAssetDetails != null)
            {
                immovableAssetTypesViewModels.Id = immovableAssetDetails.Id;
                immovableAssetTypesViewModels.Code = immovableAssetDetails.Code;
                immovableAssetTypesViewModels.Description = immovableAssetDetails.Description;
                immovableAssetTypesViewModels.Price = immovableAssetDetails.Price;
                immovableAssetTypesViewModels.Meters = immovableAssetDetails.Meters;
                immovableAssetTypesViewModels.BedroomQuantity = immovableAssetDetails.BedroomQuantity;
                immovableAssetTypesViewModels.BathroomQuantity = immovableAssetDetails.BathroomQuantity;
                immovableAssetTypesViewModels.AgentName = immovableAssetDetails.AgentId;
                immovableAssetTypesViewModels.ImmovableAssetTypeName = immovableAssetDetails.ImmovableAssetTypeName;
                immovableAssetTypesViewModels.SellTypeName = immovableAssetDetails.SellTypeName;
                immovableAssetTypesViewModels.ImprovementNames = immovableAssetDetails.ImprovementNames;
                return immovableAssetTypesViewModels;
            }


            return null;
        }


    }


}
