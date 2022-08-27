using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.Improvement;
using RealtyApp.Core.Application.Features.Improvement.Commands.CreateImprovement;
using RealtyApp.Core.Application.Features.Improvement.Commands.DeleteImprovement;
using RealtyApp.Core.Application.Features.Improvement.Commands.UpdateImprovement;
using RealtyApp.Core.Application.Features.Improvement.Queries.GetAllImprovement;
using RealtyApp.Core.Application.Features.Improvement.Queries.GetImprovementById;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RealtyApp.Presentation.WebApi.Controllers.v1
{
    [SwaggerTag(description: "Este controlador nos permite manejar a nivel del protocolo HTTTP todo lo relativo a las mejoras inmobiliarias del sistema.")]
    public class ImprovementController : BaseApiController
    {
        [Authorize(Roles = "Administrator")]
        [HttpPost("Create")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
           Summary = "Crea una tipo de immobiliario.",
           Description = "Al especificar las propiedades nos permite llevar acabo a la creación de un tipo de inmobiliario."
        )]
        public async Task<IActionResult> Create([FromBody] CreateImprovementCommand command)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest();

                if (command.Name == "string" || command.Description == "string")
                    return BadRequest("No puede dejar los valores por defecto");

                var result = await Mediator.Send(command);

                if (result == 0)
                    return StatusCode(StatusCodes.Status500InternalServerError);

                return NoContent();

            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message) { StatusCode = StatusCodes.Status500InternalServerError };
            }

        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("Update/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(UpdateImprovementResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Nos permite actualizar las propiedades para nuestras mejoras.",
            Description = "Brindan la facilidad de ajustar las descripciones, correcciones entre las mejoras."
        )]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateImprovementCommand command)
        {

            try
            {
                if (!ModelState.IsValid || id == 0 || id != command.Id)
                    return BadRequest("Revise los datos");

                if (command.Name == "string" || command.Description == "string")
                    return BadRequest("No puede dejar los valores por defecto");

                var toReturn = await Mediator.Send(command);

                if (toReturn == null)
                    return StatusCode(StatusCodes.Status500InternalServerError);

                return Ok(toReturn);

            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message) { StatusCode = StatusCodes.Status500InternalServerError };
            }

        }


        [Authorize(Roles = "Administrator,Developer")]
        [HttpGet("List")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(GetAllImprovementQuery))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
           Summary = "Nos permite traer todas las mejoras inmobiliarias en el sistema.",
           Description = "Brindan la facilidad de traer todas las mejoras inmobiliarias en el sistema..."
        )]
        public async Task<IActionResult> List()
        {

            try
            {
               var toReturn = await Mediator.Send(new GetAllImprovementQuery());

                if (toReturn == null)
                    return NotFound("No hay mejoras");

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImprovementDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Nos permite obtener una mejora mediante su identificador.",
        Description = "permite obtener una mejora mediante su identificador (ID)."
        )]
        public async Task<IActionResult> GetById(int id)
        {

            try
            {
                var result = await Mediator.Send(new GetImprovementByIdQuery() { Id = id });

                if (result == null)
                    return NotFound("No se encontró una mejora con este id");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message) { StatusCode = StatusCodes.Status500InternalServerError };
            }

        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("Delete/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
         Summary = "Nos da la opción de realizar un borrado.",
         Description = "Permite borrar las mejoras que estan en el sistema."
         )]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                var result = await Mediator.Send(new DeleteImprovementCommand() { Id = id});
                
                if (result == 0)
                    return StatusCode(StatusCodes.Status500InternalServerError);


                return NoContent();
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message) { StatusCode = StatusCodes.Status500InternalServerError };
            }

        }

    }
}
