using AutoMapper;
using MediatR;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.Agent;
using RealtyApp.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Features.Agent.Queries.GetByIdAgentProperties
{
    //<summary>
    // Permite obtener las propiedades del agente deseado mediante el ID.
    //</summary>

    public class GetAgentPropertiesByIdQuery : IRequest<AgentDTO>
    {
        [SwaggerParameter(Description = "El id del agente del que desea obtener las propiedades.")]
        public string Id { get; set; }
    }

    //Lo puse en comentarios hasta que el compañero alexander me hable de su parte de immovable asset

    //public class GetAgentPropertiesByIdHandler : IRequestHandler<GetAgentPropertiesByIdQuery, SellTypeDTO>
    //{
    //    private readonly ISellTypeRepository _sellTypeRepository;
    //    private readonly IMapper _maper;

    //    public GetAgentPropertiesByIdHandler(ISellTypeRepository sellTypeRepository, IMapper maper)
    //    {
    //        _sellTypeRepository = sellTypeRepository;
    //        _maper = maper;
    //    }

    //    public async Task<SellTypeDTO> Handle(GetSellTypeByIdQuery request, CancellationToken cancellationToken)
    //    {
    //        var result = await GetSellTypeDTOById(request.Id);

    //        if (result == null)
    //            throw new Exception($"Immovable Sell type not found.");
    //        else
    //            return result;
    //    }

    //    private async Task<SellTypeDTO> GetSellTypeDTOById(int id)
    //    {
    //        var result = await _sellTypeRepository.GetByIdAsync(id);

    //        if (result == null)
    //            return null;
    //        else
    //            return _maper.Map<SellTypeDTO>(result);
    //    }
    //}
}
