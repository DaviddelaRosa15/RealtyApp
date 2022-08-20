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

namespace RealtyApp.Core.Application.Features.Agent.Queries.GetByIdAgent
{
    //<summary>
    // Permite obtener el agente deseado mediante el ID.
    //</summary>

    public class GetAgentByIdQuery : IRequest<AgentDTO>
    {
        [SwaggerParameter(Description = "El id del agente que desea obtener.")]
        public string Id { get; set; }
    }

    public class GetAgentByIdHandler : IRequestHandler<GetAgentByIdQuery, AgentDTO>
    {
        private readonly IUserService _userService;
        private readonly IImmovableAssetService _immovableService;

        public GetAgentByIdHandler(IUserService userService, IImmovableAssetService immovableService)
        {
            _userService = userService;
            _immovableService = immovableService;
        }

        public async Task<AgentDTO> Handle(GetAgentByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await GetAgentDTOById(request.Id);

            if (result == null)
                throw new Exception($"Agent not found.");
            else
                result.PropertiesQuantity = await _immovableService.CountImmovablesByAgent(result.Id);
                return result;
        }

        private async Task<AgentDTO> GetAgentDTOById(string id)
        {
            var result = await _userService.GetAgentById(id);

            if (result == null)
                return null;
            else
                return result;
        }
    }
}
