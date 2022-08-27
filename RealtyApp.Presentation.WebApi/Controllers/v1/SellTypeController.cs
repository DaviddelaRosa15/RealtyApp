using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.SellType;
using RealtyApp.Core.Application.Features.SellType.Commands.CreateSellType;
using RealtyApp.Core.Application.Features.SellType.Commands.DeleteSellType;
using RealtyApp.Core.Application.Features.SellType.Commands.UpdateSellType;
using RealtyApp.Core.Application.Features.SellType.Queries.GetAllSellType;
using RealtyApp.Core.Application.Features.SellType.Queries.GetByIdSellType;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RealtyApp.Presentation.WebApi.Controllers.v1
{
    [SwaggerTag(description: "Este controlador nos permite manejar a nivel del protocolo HTTTP todo lo relativo a los tipos de ventas de inmuebles del sistema.")]
    public class SellTypeController : BaseApiController
    {
        [Authorize(Roles = "Administrator")]
        [HttpPost("Create")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
           Summary = "Crea un tipo de ventas.",
           Description = "Al especificar las propiedades nos permite llevar acabo a la creación de un tipo de venta."
        )]
        public async Task<IActionResult> Create([FromBody] CreateSellTypeCommand command)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest("Revise los datos");
                }

                if (command.Name == "string" || command.Description == "string")
                    return BadRequest("No puede dejar los valores por defecto");

                var result = await Mediator.Send(command);

                if (result == 0)
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

        [Authorize(Roles = "Administrator")]
        [HttpPut("Update/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(UpdateSellTypeResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Nos permite actualizar las propiedades para nuestros tipos de ventas.",
            Description = "Brindan la facilidad de ajustar las descripciones, correcciones entre los tipos de ventas."
        )]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSellTypeCommand command)
        {

            try
            {
                if (!ModelState.IsValid || id == 0 || id != command.Id)
                {
                    return BadRequest("Revise los datos");
                }

                if (command.Name == "string" || command.Description == "string")
                    return BadRequest("No puede dejar los valores por defecto");

                var toReturn = await Mediator.Send(command);

                if (toReturn == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

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
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(GetAllSellTypeQuery))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
           Summary = "Nos permite traer todos los tipos de ventas en el sistema.",
           Description = "Brindan la facilidad de traer todos los tipos de ventas en el sistema..."
        )]
        public async Task<IActionResult> List()
        {

            try
            {
                var toReturn = await Mediator.Send(new GetAllSellTypeQuery());

                if (toReturn == null)
                {
                    return NotFound("No hay tipos de ventas");
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SellTypeDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Nos permite obtener un tipo de venta mediante su identificador.",
        Description = "Permite obtener un tipo de venta mediante su identificador (ID)."
        )]
        public async Task<IActionResult> GetById(int id)
        {

            try
            {
                var result = await Mediator.Send(new GetSellTypeByIdQuery() { Id = id });

                if (result == null)
                {
                    return NotFound("No se encontró un tipo de venta con este id");
                }

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
         Summary = "Nos da la opción de realizar un borrado de un tipo de venta.",
         Description = "Permite borrar los tipos de ventas que estan en el sistema mediante su identificador (ID)."
         )]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                var result = await Mediator.Send(new DeleteSellTypeCommand() { Id = id });

                if (result == 0)
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
