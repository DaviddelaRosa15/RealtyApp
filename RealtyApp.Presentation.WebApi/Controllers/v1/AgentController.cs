using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.ImmovableAsset;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.Agent;
using RealtyApp.Core.Application.Features.Agent.Commands.ChangeStatusAgent;
using RealtyApp.Core.Application.Features.Agent.Queries.GetAllAgent;
using RealtyApp.Core.Application.Features.Agent.Queries.GetByIdAgent;
using RealtyApp.Core.Application.Features.Agent.Queries.GetByIdAgentProperties;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RealtyApp.Presentation.WebApi.Controllers.v1
{
    [SwaggerTag(description: "Este controlador nos permite manejar a nivel del protocolo HTTTP todo lo relativo a los usuarios agentes del sistema.")]
    public class AgentController : BaseApiController
    {
        [Authorize(Roles = "Administrator,Developer")]
        [HttpGet("List")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(GetAllAgentQuery))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
           Summary = "Nos permite traer todos los usuarios agentes en el sistema.",
           Description = "Brindan la facilidad de traer todos los usuarios agentes en el sistema..."
        )]
        public async Task<IActionResult> List()
        {

            try
            {
               var toReturn = await Mediator.Send(new GetAllAgentQuery());

                if (toReturn == null)
                {
                    return NotFound();
                }                    

                return Ok(toReturn);

            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message) { StatusCode = StatusCodes.Status500InternalServerError };
            }

        }

        [Authorize(Roles = "Administrator,Developer")]
        [HttpGet("GetById/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Nos permite obtener un usuario agente mediante su identificador.",
        Description = "Permite obtener un usuario agente mediante su identificador (ID)."
        )]
        public async Task<IActionResult> GetById(string id)
        {

            try
            {
                if (id == "string")
                {
                    return BadRequest();
                }                    

                var result = await Mediator.Send(new GetAgentByIdQuery() { Id = id });

                if (result == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }                    

                return Ok(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message) { StatusCode = StatusCodes.Status500InternalServerError };
            }

        }

        [Authorize(Roles = "Administrator,Developer")]
        [HttpGet("GetAgentPropertiesById/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Nos permite obtener el listado de propiedades de un agente mediante su identificador.",
        Description = "Permite obtener el listado de propiedades de un agente mediante su identificador (ID)."
        )]
        public async Task<IActionResult> GetAgentPropertiesById(string id)
        {
            try
            {
                if (id == "string")
                {
                    return BadRequest();
                }

                var result = await Mediator.Send(new GetAgentPropertiesByIdQuery() { Id = id });

                if (result == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message) { StatusCode = StatusCodes.Status500InternalServerError };
            }

        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("ChangeStatus/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Nos permite actualizar el estado para nuestros usuarios agentes.",
            Description = "Brindan la facilidad de ajustar el estado de un agente mediante su identificador (ID)."
        )]
        public async Task<IActionResult> ChangeStatus(string id, [FromBody] ChangeStatusAgentCommand command)
        {

            try
            {

                var toReturn = await Mediator.Send(command);

                if (toReturn == 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                return NoContent();

            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message) { StatusCode = StatusCodes.Status500InternalServerError };
            }

        }
    }
}
