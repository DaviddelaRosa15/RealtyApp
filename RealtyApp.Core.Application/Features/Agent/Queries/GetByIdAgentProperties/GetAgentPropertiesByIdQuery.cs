using AutoMapper;
using MediatR;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.ImmovableAsset;
using RealtyApp.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections;
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

    public class GetAgentPropertiesByIdQuery : IRequest<IEnumerable<ImmovableAssetDTO>>
    {
        [SwaggerParameter(Description = "El id del agente del que desea obtener las propiedades.")]
        public string Id { get; set; }
    }

    public class GetAgentPropertiesByIdHandler : IRequestHandler<GetAgentPropertiesByIdQuery, IEnumerable<ImmovableAssetDTO>>
    {
        private readonly IImmovableAssetService _immovableService;
        private readonly IUserService _userService;
        private readonly IMapper _maper;

        public GetAgentPropertiesByIdHandler(IImmovableAssetService immovableService, IUserService userService, IMapper maper)
        {
            _immovableService = immovableService;
            _userService = userService;
            _maper = maper;
        }

        public async Task<IEnumerable<ImmovableAssetDTO>> Handle(GetAgentPropertiesByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await GetAgentPropertiesDTOById(request.Id);

            if (result == null || result.Count == 0)
                return null;
            else
                return result;
        }

        private async Task<List<ImmovableAssetDTO>> GetAgentPropertiesDTOById(string id)
        {
            var agent = await _userService.GetAgentById(id);
            if(agent == null)
                throw new Exception($"No se encontró este agente en el sistema...");

            var result = await _immovableService.GetIncludeDetails();

            if (result == null)
                throw new Exception($"No se encontraron propiedades en el sistema...");
            else
            {
                var immovables = result.Where(x => x.AgentId == id).ToList();
                return _maper.Map <List<ImmovableAssetDTO>>(immovables); ;
            }                
        }
    }
}
