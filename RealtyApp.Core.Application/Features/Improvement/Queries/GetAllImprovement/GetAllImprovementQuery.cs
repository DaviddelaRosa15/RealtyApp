using AutoMapper;
using MediatR;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.Improvement;
using RealtyApp.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Features.Improvement.Queries.GetAllImprovement
{
    /// <summary>
    /// Permite recoger todas las mejoras que estan disponibles en el sistema.
    /// </summary>

    public class GetAllImprovementQuery : IRequest<ICollection<ImprovementDTO>>
    {

    }

    public class GetAllImprovementQueryHandler : IRequestHandler<GetAllImprovementQuery, ICollection<ImprovementDTO>>
    {
        private readonly IImprovementRepository _improvementRepository;
        private readonly IMapper _mapper;

        public GetAllImprovementQueryHandler(IImprovementRepository improvementRepository, IMapper mapper)
        {
            _improvementRepository = improvementRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<ImprovementDTO>> Handle(GetAllImprovementQuery request, CancellationToken cancellationToken)
        {

            var result = await GetAllImprovementDtoWithIncludes();

            if (result == null || result.Count == 0)
                return null;

            return result;

        }

        private async Task<List<ImprovementDTO>> GetAllImprovementDtoWithIncludes()
        {
            var improvements = await _improvementRepository.GetAllAsync();

            List<ImprovementDTO> improvementViews = _mapper.Map<List<ImprovementDTO>>(improvements);

            return improvementViews;
        }

    }

}
