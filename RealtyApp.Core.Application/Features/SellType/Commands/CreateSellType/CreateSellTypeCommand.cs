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

namespace RealtyApp.Core.Application.Features.SellType.Commands.CreateSellType
{
    /// <summary>
     /// Nos permite crear un nuevo tipo de venta de inmueble, especificando la información descrita en la documentación.
    /// </summary>  
    public class CreateSellTypeCommand : IRequest<int>
    {
        /// <example>Renta</example>
        [SwaggerParameter(Description = "Nombre que deseamos destinar para nuestro nuevo tipo/clase de inmueble.")]
        [Required(ErrorMessage = "Debe de especificar un nombre para este tipo/clase de inmueble.")]
        public string Name { get; set; }

        /// <example>Se paga la vivienda periodicamente</example>
        [SwaggerParameter(Description = "Descripción destinada para el tipo de inmueble.")]
        [Required(ErrorMessage = "Debe de especificar una descripción para este tipo de inmueble.")]
        public string Description { get; set; }
    }

    public class CreateSellTypeCommandHandler : IRequestHandler<CreateSellTypeCommand, int>
    {
        private readonly ISellTypeRepository _sellTypeRepository;
        private readonly IMapper _mapper;

        public CreateSellTypeCommandHandler(ISellTypeRepository sellTypeRepository, IMapper mapper)
        {
            _sellTypeRepository = sellTypeRepository;
            _mapper = mapper;
        }


        public async Task<int> Handle(CreateSellTypeCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var valueToAdd = _mapper.Map<RealtyApp.Core.Domain.Entities.SellType>(command);
                valueToAdd = await _sellTypeRepository.AddAsync(valueToAdd);
                return valueToAdd.Id;
            }
            catch (Exception)
            {
               throw new Exception("Error at time to create this new Immovable Sell Type.");
            }
        }

    }


}
