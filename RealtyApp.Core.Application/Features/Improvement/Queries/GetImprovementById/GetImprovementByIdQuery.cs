using AutoMapper;
using MediatR;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.Improvement;
using RealtyApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Features.Improvement.Queries.GetImprovementById
{
    /// <summary>
    /// Este enpoint permite obtener una mejora inmobiliaria mediante su identificador/id
    /// </summary>
    public class GetImprovementByIdQuery : IRequest<ImprovementDTO>
    {
        [SwaggerParameter(Description = "Identificador(Id) del la mejora a obtener.", Required = true)]
        ///<example>1</example>
        public int Id { get; set; }

    }

    public class GetImprovementByIdQueryHandler : IRequestHandler<GetImprovementByIdQuery, ImprovementDTO>
    {
        private readonly IImprovementRepository _improvementRepository;
        private readonly IMapper _mapper;

        public GetImprovementByIdQueryHandler(IImprovementRepository improvementRepository, IMapper mapper)
        {
            _improvementRepository = improvementRepository;
            _mapper = mapper;
        }

        public async Task<ImprovementDTO> Handle(GetImprovementByIdQuery request, CancellationToken cancellationToken)
        {

            var result = await _improvementRepository.GetByIdAsync(request.Id);

            if (result == null)
                return null;
            else
                return _mapper.Map<ImprovementDTO>(result);

        }

    }


}
