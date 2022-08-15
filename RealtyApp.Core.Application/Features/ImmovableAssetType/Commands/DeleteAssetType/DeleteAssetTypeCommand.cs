using MediatR;
using RealtyApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Features.ImmovableAssetType.Commands.DeleteAssetType
{
    /// <summary>
    /// Elimina permanentemente el tipo de inmobiliario que haya especificado por ID.
    /// </summary>
    public class DeleteAssetTypeCommand : IRequest<int>
    {
        [SwaggerParameter(Description = "Identificador/Id del inmobiliario a eliminar.", Required = true)]
        public int Id { get; set; }
    }

    public class DeleteAssetTypeCommandHandler : IRequestHandler<DeleteAssetTypeCommand, int>
    {
        private readonly IImmovableAssetTypeRepository _assetTypeRepository;
        public DeleteAssetTypeCommandHandler(IImmovableAssetTypeRepository repository)
        {
            this._assetTypeRepository = repository;
        }

        public async Task<int> Handle(DeleteAssetTypeCommand command, CancellationToken cancellationToken)
        {
            var result = await _assetTypeRepository.GetByIdAsync(command.Id);

            if (result == null)
                throw new Exception("No existe dicho tipo de inmobiliario.");

            await _assetTypeRepository.DeleteAsync(result);

            return result.Id;
        }

    }


}
