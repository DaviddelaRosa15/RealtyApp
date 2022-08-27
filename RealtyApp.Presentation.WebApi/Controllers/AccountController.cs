using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealtyApp.Core.Application.Dtos.Account;
using RealtyApp.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RealtyApp.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Sistema de membresia")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("authenticate")]
        [SwaggerOperation(
            Summary = "Login de usuario",
            Description = "Autentica un usuario en el sistema y le retorna un JWT"
        )]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(AuthenticationResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(AuthenticationResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(AuthenticationResponse))]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticationRequest request)
        {
            var response = await _accountService.AuthenticateAsyncWebApi(request);
            if (response.HasError)
            {
                if (response.Error == "Usted no tiene permisos para usar la Api de RealtyApp")
                {
                    return StatusCode(StatusCodes.Status403Forbidden,response.Error);
                }
                return BadRequest(response);                
            }
            return Ok(response);
        }

        #region [Developer & Administrator Registration]

        [HttpPost("Register/Developer")]
        [SwaggerOperation(
            Summary = "Creación de usuario tipo Desarrollador.",
            Description = "Recibe los parametros necesarios para crear un usuario con el rol desarrollador."
        )]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RegisterResponse))]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterDeveloperAsync([FromBody] RegisterRequest request)
        {
            try
            {
                RegisterResponse result = await _accountService.RegisterDeveloperUserAsync(request);

               
                if (result.HasError || !ModelState.IsValid)
                {
                    result.Error += string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                    return BadRequest(result);
                }


                return StatusCode(StatusCodes.Status201Created);

            }
            catch (System.Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

        }

        [Authorize(Roles ="Administrator")]
        [HttpPost("Register/Administrator")]
        [SwaggerOperation(
            Summary = "Creación de usuario tipo administrador.",
            Description = "Recibe los parametros necesarios para crear un usuario con el rol administrador."
        )]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RegisterResponse))]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterAdministratorAsync([FromBody] RegisterRequest request)
        {
            try
            {
                var result = await _accountService.RegisterAdministratorUserAsync(request);

                if (result.HasError || !ModelState.IsValid)
                {
                    result.Error += string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                    return BadRequest(result);
                }

                return StatusCode(StatusCodes.Status201Created);

            }
            catch (System.Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

        }

        #endregion
    }
}
