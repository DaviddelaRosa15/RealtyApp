using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.ImmovableAssetType;
using RealtyApp.Core.Application.Features.ImmovableAssetType.Commands.CreateAssetType;
using RealtyApp.Core.Application.Features.ImmovableAssetType.Commands.DeleteAssetType;
using RealtyApp.Core.Application.Features.ImmovableAssetType.Commands.UpdateAssetType;
using RealtyApp.Core.Application.Features.ImmovableAssetType.Queries.GetAllAssetType;
using RealtyApp.Core.Application.Features.ImmovableAssetType.Queries.GetByIdAssetType;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RealtyApp.Presentation.WebApi.Controllers.v1
{
    [SwaggerTag(description:"Controla todas las operaciones de mantenimiento de tipo de inmobiliarios, en el sistema.")]
    public class ImmovableAssetTypeController : BaseApiController
    {
        [Authorize(Roles = "Administrator")]
        [HttpPost("Create")]
        [SwaggerOperation(
           Summary = "Crea una tipo de immobiliario.",
            Description = "Al especificar las propiedades nos permite llevar acabo a la creación de un tipo de inmobiliario."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAssetType([FromBody] CreateAssetTypeCommand command)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (command.Name == "string" || command.Description == "string")
                    return BadRequest("No puede dejar los valores por defecto");

                var result = await Mediator.Send(command);

                if (result == 0)
                    return StatusCode( StatusCodes.Status500InternalServerError );

                return NoContent();

            }
            catch (Exception e)
            {
                return new JsonResult(e.Message) { StatusCode = StatusCodes.Status500InternalServerError };
            }

        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("Update/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Permite actualizar un tipo de inmobiliario.",
            Description = "Maneja el apartado de actualización, debe de especificar los parametros correspondientes."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateAssetTypeResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAssetType(int id, [FromBody] UpdateAssetTypeCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (id != command.Id)
                    return BadRequest("No ingreso los mismos identificadores");

                if (command.Name == "string" || command.Description == "string")
                    return BadRequest("No puede dejar los valores por defecto");

                var result = await Mediator.Send(command);

                if (result == null)
                    return StatusCode(StatusCodes.Status500InternalServerError);

                return Ok(result);

            }
            catch (Exception e)
            {
                return new JsonResult(e.Message) { StatusCode = StatusCodes.Status500InternalServerError };
            }

        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("Delete/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Permite eliminar un tipo de inmobiliario con sus respectivos inmobiliarios asociados.",
            Description = "Maneja el apartado de eliminación, debe de especificar los parametros correspondientes."
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAssetType(int id)
        {
            try
            {
                var result = await Mediator.Send(new DeleteAssetTypeCommand() { Id = id} );

                if (result == 0)
                    return StatusCode(StatusCodes.Status500InternalServerError);

                return NoContent();

            }
            catch (Exception e)
            {
                return new JsonResult(e.Message) { StatusCode = StatusCodes.Status500InternalServerError };
            }

        }

        [Authorize(Roles = "Administrator,Developer")]
        [HttpGet("List")]
        [SwaggerOperation(
            Summary = "Todos los tipos de inmobiliarios.",
            Description = "Permite obtener todas las clases de inmobiliarios registrados en el sistema."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImmovableAssetTypeDTO) )]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            //Este controlador no trabajara hasta que se hagan las respectivas configuraciones en Automapper de las que depende ImmovableAssetType.ImmovableAsset.IdUser
            //Y las demas properties....
            try
            {
              var result = await Mediator.Send(new GetAllAssetTypeQuery());

              if (result == null)
                  return NotFound("No hay tipos de propiedades");

              return Ok(result);

            }
            catch (Exception e)
            {
                return new JsonResult(e.Message) { StatusCode = StatusCodes.Status500InternalServerError };
            }

        }

        [Authorize(Roles = "Administrator,Developer")]
        [HttpGet("GetById/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Permite obtener un tipo de inmobiliario con sus respectivos ID.",
            Description = "Provee mecanismos de identificación para encontrar un tipo de inmobiliario."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImmovableAssetTypeDTO) )]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAssetType(int id)
        {
            try
            {
                var result = await Mediator.Send(new GetImmovableAssetTypeByIdQuery() { Id = id });

                if (result == null)
                    return NotFound("No se encontró un tipo de propiedad con este id");

                return Ok(result);

            }
            catch (Exception e)
            {
                return new JsonResult(e.Message) { StatusCode = StatusCodes.Status500InternalServerError };
            }

        }
    }
}
