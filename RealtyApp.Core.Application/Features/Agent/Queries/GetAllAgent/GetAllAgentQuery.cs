using AutoMapper;
using MediatR;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.Agent;
using RealtyApp.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Features.Agent.Queries.GetAllAgent
{
    //<summary>
    // Permite obtener todos los agentes registrados en el sistema.
    //</summary>

    public class GetAllAgentQuery : IRequest<IEnumerable<AgentDTO>>
    {

    }

    public class GetAllAgentQueryHandler : IRequestHandler<GetAllAgentQuery, IEnumerable<AgentDTO>>
    {

        private readonly IUserService _userService;

        public GetAllAgentQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IEnumerable<AgentDTO>> Handle(GetAllAgentQuery request, CancellationToken cancellationToken)
        {
           var result = await GetAllDTOs();

            if (result == null)
                throw new Exception("Agents not found....");
            else
                return result;
        }

        private async Task<List<AgentDTO>> GetAllDTOs()
        {

            var agents = await _userService.GetAllAgents();

            return agents;
        }

    }
}
