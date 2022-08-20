using AutoMapper;
using MediatR;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.ImmovableAsset;
using RealtyApp.Core.Application.Interfaces.Repositories;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Application.ViewModels.ImmovableAsset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Features.ImmovableAsset.Queries.GetAllImmovableAsset
{

    /// <summary>
    /// Obtiene todas las propiedades disponibles en el sistema.
    /// </summary>
    public class GetAllImmovableAssetQuery: IRequest<IEnumerable<ImmovableAssetDTO>>
   {

   }

    public class GetAllImmovableAssetHandler : IRequestHandler<GetAllImmovableAssetQuery, IEnumerable<ImmovableAssetDTO>>
    {

        private readonly IImmovableAssetService _immovableAssetService;       
        private readonly IMapper _mapper;
        public GetAllImmovableAssetHandler(IImmovableAssetRepository immovableAssetRepository
            , IMapper mapper
            , IImmovableAssetService immovableAssetService)
        {
            _immovableAssetService = immovableAssetService;           
            _mapper = mapper;
        }

        public async Task<IEnumerable<ImmovableAssetDTO>> Handle(GetAllImmovableAssetQuery request, CancellationToken cancellationToken)
        {
            var immovableAssets = await GetIncludeDetails();
            return immovableAssets;
        }


        private async Task<List<ImmovableAssetDTO>> GetIncludeDetails()
        {
            List<DetailsViewModelApi> immovableAssetDetails = await _immovableAssetService.GetIncludeDetails();
            List<ImmovableAssetDTO> immovableAssetTypesViewModels = new();
            foreach (DetailsViewModelApi asset in immovableAssetDetails)
            {
                immovableAssetTypesViewModels.Add(new ImmovableAssetDTO()
                {
                    Id = asset.Id,
                    Code = asset.Code,
                    Description = asset.Description,
                    Price = asset.Price,
                    Meters = asset.Meters,
                    BedroomQuantity = asset.BedroomQuantity,
                    BathroomQuantity = asset.BathroomQuantity,
                    AgentName = asset.AgentId,
                    ImmovableAssetTypeName = asset.ImmovableAssetTypeName,
                    SellTypeName = asset.SellTypeName,
                    ImprovementNames = asset.ImprovementNames
                });
            }            
           
            return immovableAssetTypesViewModels;
        }
    }



}
