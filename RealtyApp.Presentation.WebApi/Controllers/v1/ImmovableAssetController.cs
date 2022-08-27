using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.ImmovableAsset;
using RealtyApp.Core.Application.Features.ImmovableAsset.Queries.GetAllImmovableAsset;
using RealtyApp.Core.Application.Features.ImmovableAsset.Queries.GetAllImmovableAssetById;
using RealtyApp.Core.Application.Features.ImmovableAsset.Queries.GetGetAllImmovableAssetByCode;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RealtyApp.Presentation.WebApi.Controllers.v1
{
    [SwaggerTag(description: "Nos permite filtrar las propiedades" +
        " de distintas maneras: Todas, por id o por codigo")]
    [Authorize(Roles = "Administrator,Developer")]
    public class ImmovableAssetController : BaseApiController
    {       
        [HttpGet("List")]
        [SwaggerOperation(
           Summary = "Obtiene todas las propiedades.",
            Description = "Nos muestras las propiedades con todos sus componentes, como: Mejoras, tipo de venta etc..."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImmovableAssetDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {            
            try
            {
                var result = await Mediator.Send(new GetAllImmovableAssetQuery());

                if (result == null)
                    return NotFound("No hay inmuebles");

                return Ok(result);

            }
            catch (Exception e)
            {
                return new JsonResult(e.Message) { StatusCode = StatusCodes.Status500InternalServerError };
            }

        }

        [HttpGet("GetById/{id}")]
        [SwaggerOperation(
           Summary = "Obtiene una propiedad por un id especificado",
            Description = "Si el id introducido existe, nos muestra esa" +
            " propiedad con todas sus propiedades, si no existe pues devuelve un notFound "
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImmovableAssetDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {

            try
            {
                var result = await Mediator.Send(new GetImmovableAssetByIdQuery() { Id = id });

                if (result == null)
                {
                    return NotFound("No se encontró un inmueble con este id");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message) { StatusCode = StatusCodes.Status500InternalServerError };
            }

        }

        [HttpGet("GetByCode/{code}")]
        [SwaggerOperation(
           Summary = "Obtiene una propiedad por un código especificado",
            Description = "Si el código introducido existe, nos muestra esa" +
            " propiedad con todas sus propiedades, si no existe pues devuelve un notFound "
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImmovableAssetDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCode(string code)
        {
            try
            {
                var result = await Mediator.Send(new GetImmovableAssetByCodeQuery() { Code = code });

                if (result == null)
                {
                    return NotFound("No se encontró un inmueble con este código");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message) { StatusCode = StatusCodes.Status500InternalServerError };
            }

        }
    }
}
