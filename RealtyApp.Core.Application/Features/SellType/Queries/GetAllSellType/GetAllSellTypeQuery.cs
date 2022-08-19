using AutoMapper;
using MediatR;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.SellType;
using RealtyApp.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Features.SellType.Queries.GetAllSellType
{
    //<summary>
    // Permite obtener todos los tipos de ventas de inmuebles registrados en el sistema.
    //</summary>

    public class GetAllSellTypeQuery : IRequest<IEnumerable<SellTypeDTO>>
    {

    }

    public class GetAllSellTypeQueryHandler : IRequestHandler<GetAllSellTypeQuery, IEnumerable<SellTypeDTO>>
    {

        private readonly ISellTypeRepository _sellTypeRepository;
        private readonly IMapper _mapper;

        public GetAllSellTypeQueryHandler(ISellTypeRepository sellTypeRepository, IMapper maper)
        {
            _sellTypeRepository = sellTypeRepository;
            _mapper = maper;
        }

        public async Task<IEnumerable<SellTypeDTO>> Handle(GetAllSellTypeQuery request, CancellationToken cancellationToken)
        {
           var result = await GetAllDTOsWithIncludes();

            if (result == null)
                throw new Exception("Immovables Asset Types not found....");
            else
                return result;
        }

        private async Task<List<SellTypeDTO>> GetAllDTOsWithIncludes()
        {

            var sellTypes = await _sellTypeRepository.GetAllWithIncludeAsync(new List<string>() { });

            List<SellTypeDTO> sellTypesDtos = _mapper.Map<List<SellTypeDTO>>(sellTypes);

            return sellTypesDtos;
        }

    }
}
