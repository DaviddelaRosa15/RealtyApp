using AutoMapper;
using MediatR;
using RealtyApp.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Features.Agent.Commands.ChangeStatusAgent
{
    /// <summary>
    /// Nos permite llevar a cabo la actualización en los tipos de ventas de inmobiliarios creados en el sistema.
    /// </summary>
    public class ChangeStatusAgentCommand : IRequest<int>
    {
        [SwaggerParameter(Description = "Hace referencia al identificador del agente en el sistema.",Required = true)]
        [Required]
        ///<example>1</example>
        public string Id { get; set; }

        /// <example>Renta</example>
        [SwaggerParameter(Description = "Estado en el que queremos poner al agente en cuestion", Required = true)]
        [Required(ErrorMessage = "Debe de especificar un estado para este agente.")]
        public string Status { get; set; }
    }

    public class ChangeStatusAgentCommandHandler : IRequestHandler<ChangeStatusAgentCommand, int>
    {
        private readonly IUserService _userService;
        public ChangeStatusAgentCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<int> Handle(ChangeStatusAgentCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var agent = await _userService.GetUserById(command.Id);

                if (agent == null)
                    throw new Exception("Agente no encontrado.");              

                var result = await _userService.ChangeUserStatus(agent.Id, command.Status);

                if (result)
                    return 1;
                else
                    return 0;

            }
            catch (Exception)
            {
                throw new Exception("Error al momento de realizar la actualizacion, si persiste contactenos.");                
            }

        }

    }
}
