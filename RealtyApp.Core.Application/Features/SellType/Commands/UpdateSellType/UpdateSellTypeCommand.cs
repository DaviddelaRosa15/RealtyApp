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

namespace RealtyApp.Core.Application.Features.SellType.Commands.UpdateSellType
{
    /// <summary>
    /// Nos permite llevar a cabo la actualización en los tipos de ventas de inmobiliarios creados en el sistema.
    /// </summary>
    public class UpdateSellTypeCommand : IRequest<UpdateSellTypeResponse>
    {
        [SwaggerParameter(Description = "Hace referencia al identificador del tipo de venta en el sistema.",Required = true)]
        [Required]
        ///<example>1</example>
        public int Id { get; set; }

        /// <example>Renta</example>
        [SwaggerParameter(Description = "Nombre que deseamos destinar para nuestro nuevo tipo de venta de inmueble.", Required = true)]
        [Required(ErrorMessage = "Debe de especificar un nombre para este tipo de venta de inmueble.")]
        public string Name { get; set; }

        ///<example>Pago mensual por seguir habitando este hogar...</example>
        [SwaggerParameter(Description = "Descripción destinada para el tipo de venta de inmueble.", Required = true)]
        [Required(ErrorMessage = "Debe de especificar una descripción para este tipo de venta de inmueble.")]
        public string Description { get; set; }

    }

    public class UpdateSellTypeCommandHandler : IRequestHandler<UpdateSellTypeCommand, UpdateSellTypeResponse>
    {
        private readonly ISellTypeRepository _sellTypeRepository;
        private readonly IMapper _mapper;
        public UpdateSellTypeCommandHandler(ISellTypeRepository sellTypeRepository, IMapper mapper)
        {
            _sellTypeRepository = sellTypeRepository;
            _mapper = mapper;
        }

        public async Task<UpdateSellTypeResponse> Handle(UpdateSellTypeCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var gettingOldModel = await _sellTypeRepository.GetByIdAsync(command.Id);

                if (gettingOldModel == null)
                    throw new Exception("No se encontró el tipo de venta.");

                var updatedValues = _mapper.Map<RealtyApp.Core.Domain.Entities.SellType>(command);

                await _sellTypeRepository.UpdateAsync(updatedValues, gettingOldModel.Id);

                //return _mapper.Map<UpdateAssetTypeResponse>(updatedValues);

                return new UpdateSellTypeResponse()
                {
                    Id = updatedValues.Id,
                    Description = updatedValues.Description,
                    Name = updatedValues.Name
                };


            }
            catch (Exception)
            {
                throw new Exception("Error al momento de realizar la actualizacion, si persiste contactenos.");                
            }

        }

    }
}
