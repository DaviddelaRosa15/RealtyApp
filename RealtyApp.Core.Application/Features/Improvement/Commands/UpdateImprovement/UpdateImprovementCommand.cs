using AutoMapper;
using MediatR;
using RealtyApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Features.Improvement.Commands.UpdateImprovement
{
    /// <summary>
    /// Este endpoint nos permite, actualizar los valores de alguna mejora en especial, mediante su id.
    /// </summary>

    public class UpdateImprovementCommand : IRequest<UpdateImprovementResponse>
    {
        [SwaggerParameter(Description = "Es el identificador o codigo de la mejora inmobiliaria.")]
        [Required]
        public int Id { get; set; }

        [SwaggerParameter(Description = "Es el nuevo nombre que contendra nuestra mejora inmobiliaria", Required = true)]
        [Required(ErrorMessage = "Debe de especificar el un nombre para esta mejora.")]
        ///<example>Jacuzzi Upgrade</example>
        public string Name { get; set; }

        [SwaggerParameter(Description = "Mas detalles sobre la mejora...", Required = true)]
        [Required(ErrorMessage = "Debe de especificar una descripción para esta mejora.")]
        public string Description { get; set; }

    }


    public class UpdateImprovementCommandHandler : IRequestHandler<UpdateImprovementCommand, UpdateImprovementResponse>
    {
        private readonly IImprovementRepository _improvementRepository;
        private readonly IMapper _mapper;

        public UpdateImprovementCommandHandler(IImprovementRepository improvementRepository, IMapper mapper)
        {
            _improvementRepository = improvementRepository;
            _mapper = mapper;
        }

        public async Task<UpdateImprovementResponse> Handle(UpdateImprovementCommand command, CancellationToken cancellationToken)
        {

            var newValueToUpdate = _mapper.Map<RealtyApp.Core.Domain.Entities.Improvement>(command);

            var valueToUpdate = await _improvementRepository.GetByIdAsync(command.Id);

            if (valueToUpdate == null)
                throw new Exception("Error, valor a actualizar no encontrado.");


            await _improvementRepository.UpdateAsync(newValueToUpdate, valueToUpdate.Id);

            return _mapper.Map<UpdateImprovementResponse>(newValueToUpdate);

        }


    }




}
