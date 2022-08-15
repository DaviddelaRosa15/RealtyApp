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

namespace RealtyApp.Core.Application.Features.ImmovableAssetType.Commands.UpdateAssetType
{
    /// <summary>
    /// Nos permite llevar a cabo la actualización en los tipos de inmobiliarios creados en el sistema.
    /// </summary>
    public class UpdateAssetTypeCommand : IRequest<UpdateAssetTypeResponse>
    {
        [SwaggerParameter(Description = "Hace referencia al identificador del tipo de inmueble en el sistema.",Required = true)]
        ///<example>1</example>
        public int Id { get; set; }

        /// <example>Casa de alquiler</example>
        [SwaggerParameter(Description = "Nombre que deseamos destinar para nuestro nuevo tipo/clase de inmueble.", Required = true)]
        public string Name { get; set; }

        ///<example>Espacio confortable...</example>
        [SwaggerParameter(Description = "Descripción destinada para el tipo de inmueble.", Required = true)]
        public string Description { get; set; }

    }

    public class UpdateAssetTypeCommandHandler : IRequestHandler<UpdateAssetTypeCommand, UpdateAssetTypeResponse>
    {
        private readonly IImmovableAssetTypeRepository _assetTypeRepository;
        private readonly IMapper _mapper;
        public UpdateAssetTypeCommandHandler(IImmovableAssetTypeRepository assetTypeRepository, IMapper mapper)
        {
            _assetTypeRepository = assetTypeRepository;
            _mapper = mapper;
        }

        public async Task<UpdateAssetTypeResponse> Handle(UpdateAssetTypeCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var gettingModel = await _assetTypeRepository.GetByIdAsync(command.Id);

                if (gettingModel == null)
                    throw new Exception("Inmueble no encontrado.");

                var updatedValues = _mapper.Map<RealtyApp.Core.Domain.Entities.ImmovableAssetType>(command);

                await _assetTypeRepository.UpdateAsync(updatedValues, gettingModel.Id);

                return _mapper.Map<UpdateAssetTypeResponse>(updatedValues);

            }
            catch (Exception)
            {
                throw new Exception("Error al momento de realizar la actualizacion, si persiste contacte nos.");                
            }

        }

    }


}
