using AutoMapper;
using MediatR;
using RealtyApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Features.Improvement.Commands.CreateImprovement
{
    /// <summary>
    /// Este endpoint nos permite crear una nueva mejora en el sistema.
    /// </summary>
    public class CreateImprovementCommand : IRequest<int>
    {
        [JsonIgnore]
        [SwaggerParameter(Description = "Opcional autogenerado, es el identificador o codigo para nuestra mejora inmobiliaria.")]
        public int Id { get; set; }

        [SwaggerParameter(Description = "Es el nombre que contendra nuestra mejora inmobiliaria", Required = true)]
        [Required(ErrorMessage = "Debe de especificar el un nombre para esta mejora.")]
        ///<example>Jacuzzi</example>
        public string Name { get; set; }

        [SwaggerParameter(Description = "Mas detalles sobre la mejora...", Required = true)]
        [Required(ErrorMessage = "Debe de especificar una descripción para esta mejora.")]
        public string Description { get; set; }
    }


    public class CreateImprovementCommandHandler : IRequestHandler<CreateImprovementCommand, int>
    {
        private readonly IImprovementRepository _improvementRepository;
        private readonly IMapper _mapper;

        public CreateImprovementCommandHandler(IImprovementRepository improvementRepository, IMapper mapper)
        {
            _improvementRepository = improvementRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateImprovementCommand command, CancellationToken cancellationToken)
        {

            var valueMapped = _mapper.Map<RealtyApp.Core.Domain.Entities.Improvement>(command);

            valueMapped = await _improvementRepository.AddAsync(valueMapped);

            return valueMapped.Id;

        }


    }


}
