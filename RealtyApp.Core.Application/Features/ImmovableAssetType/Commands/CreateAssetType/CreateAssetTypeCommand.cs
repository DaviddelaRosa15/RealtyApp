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
using RealtyApp.Core.Domain.Entities;

namespace RealtyApp.Core.Application.Features.ImmovableAssetType.Commands.CreateAssetType
{
    /// <summary>
     /// Nos permite crear un nuevo tipo de inmueble, especificando la información descrita en la documentación.
    /// </summary>  
    public class CreateAssetTypeCommand : IRequest<int>
    {
         public int Id { get; set; }

        /// <example>Casa de alquiler</example>
        [SwaggerParameter(Description = "Nombre que deseamos destinar para nuestro nuevo tipo/clase de inmueble.")]
        [Required(ErrorMessage = "Debe de especificar un nombre para este tipo/clase de inmueble.")]
        public string Name { get; set; }

        [SwaggerParameter(Description = "Descripción destinada para el tipo de inmueble.")]
        [Required(ErrorMessage = "Debe de especificar una descripción para este tipo de inmueble.")]
        public string Description { get; set; }
    }

    public class CreateAssetTypeCommandHandler : IRequestHandler<CreateAssetTypeCommand, int>
    {
        private readonly IImmovableAssetTypeRepository _assetTypeRepository;
        private readonly IMapper _mapper;

        public CreateAssetTypeCommandHandler(IImmovableAssetTypeRepository assetTypeRepository, IMapper mapper)
        {
            _assetTypeRepository = assetTypeRepository;
            _mapper = mapper;
        }


        public async Task<int> Handle(CreateAssetTypeCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var valueToAdd = _mapper.Map<RealtyApp.Core.Domain.Entities.ImmovableAssetType>(command);
                valueToAdd = await _assetTypeRepository.AddAsync(valueToAdd);
                return valueToAdd.Id;
            }
            catch (Exception)
            {
               throw new Exception("Error at time to create this new Immovable Asset Type.");
            }
        }

    }


}
