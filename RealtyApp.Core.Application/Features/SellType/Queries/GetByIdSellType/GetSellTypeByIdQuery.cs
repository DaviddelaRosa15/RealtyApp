using AutoMapper;
using MediatR;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.SellType;
using RealtyApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Features.SellType.Queries.GetByIdSellType
{
    //<summary>
    // Permite obtener el tipo de venta de inmueble deseado mediante el ID.
    //</summary>

    public class GetSellTypeByIdQuery : IRequest<SellTypeDTO>
    {
        [SwaggerParameter(Description = "El id del tipo de venta de inmueble que desea obtener.")]
        public int Id { get; set; }
    }

    public class GetSellTypeByIdHandler : IRequestHandler<GetSellTypeByIdQuery, SellTypeDTO>
    {
        private readonly ISellTypeRepository _sellTypeRepository;
        private readonly IMapper _maper;

        public GetSellTypeByIdHandler(ISellTypeRepository sellTypeRepository, IMapper maper)
        {
            _sellTypeRepository = sellTypeRepository;
            _maper = maper;
        }

        public async Task<SellTypeDTO> Handle(GetSellTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await GetSellTypeDTOById(request.Id);
            
            if (result == null)
                 throw new Exception($"Immovable Sell type not found.");
            else
                return result;
        }

        private async Task<SellTypeDTO> GetSellTypeDTOById(int id)
        {
            var result = await _sellTypeRepository.GetByIdAsync(id);
            
            if (result == null)
                return null;
            else
                return _maper.Map<SellTypeDTO>(result);
        }
    }
}
