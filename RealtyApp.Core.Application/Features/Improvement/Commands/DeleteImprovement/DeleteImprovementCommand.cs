using AutoMapper;
using MediatR;
using RealtyApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Features.Improvement.Commands.DeleteImprovement
{
    /// <summary>
    /// Mediante el identificador/id permite la eliminación de alguna mejora inmobiliaria.
    /// </summary>

    public class DeleteImprovementCommand : IRequest<int>
    {
        [SwaggerParameter(Description = "Es el identificador de la mejora inmobiliaria que desea eliminar.", Required = true)]
        public int Id { get; set; }
    }


    public class DeleteImprovementCommandHandler : IRequestHandler<DeleteImprovementCommand, int>
    {
        private readonly IImprovementRepository _improvementRepository;
        private readonly IMapper _mapper;

        public DeleteImprovementCommandHandler(IImprovementRepository improvementRepository, IMapper mapper)
        {
            _improvementRepository = improvementRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteImprovementCommand command, CancellationToken cancellationToken)
        {

            var result = await _improvementRepository.GetByIdAsync(command.Id);

            if (result == null)
                throw new Exception("Mejora no encontrada, asegurece de introduccir un identificador valido.");

            await _improvementRepository.DeleteAsync(result);

            return result.Id;
        }

    }

}
