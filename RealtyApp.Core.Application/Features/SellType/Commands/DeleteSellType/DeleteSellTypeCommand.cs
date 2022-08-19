using MediatR;
using RealtyApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Features.SellType.Commands.DeleteSellType
{
    /// <summary>
    /// Elimina permanentemente el tipo de inmobiliario que haya especificado por ID.
    /// </summary>
    public class DeleteSellTypeCommand : IRequest<int>
    {
        [SwaggerParameter(Description = "Identificador/Id del tipo de venta a eliminar.", Required = true)]
        public int Id { get; set; }
    }

    public class DeleteSellTypeCommandHandler : IRequestHandler<DeleteSellTypeCommand, int>
    {
        private readonly ISellTypeRepository _sellTypeRepository;
        public DeleteSellTypeCommandHandler(ISellTypeRepository repository)
        {
            this._sellTypeRepository = repository;
        }

        public async Task<int> Handle(DeleteSellTypeCommand command, CancellationToken cancellationToken)
        {
            var result = await _sellTypeRepository.GetByIdAsync(command.Id);

            if (result == null)
                throw new Exception("No existe dicho tipo de venta.");

            await _sellTypeRepository.DeleteAsync(result);

            return result.Id;
        }

    }


}
