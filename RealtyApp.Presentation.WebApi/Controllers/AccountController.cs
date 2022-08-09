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
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticateAsync(request));
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
                var origin = Request.Headers["origin"];
                RegisterResponse result = await _accountService.RegisterDeveloperUserAsync(request, origin);

               
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
                var origin = Request.Headers["origin"];
                var result = await _accountService.RegisterAdministratorUserAsync(request, origin);

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


        [HttpGet("confirm-email")]
        [SwaggerOperation(
            Summary = "Confirmacion de usuario",
            Description = "Permite activar un usuario nuevo"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> ConfirmAccountAsync([FromQuery] string userId , [FromQuery]string token)
        {          
            return Ok(await _accountService.ConfirmAccountAsync(userId, token));
        }


        [HttpPost("forgot-password")]
        [SwaggerOperation(
            Summary = "Recordar contrasenia",
            Description = "Permite al usuario iniciar el proceso para obtener una nueva contrasenia"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.ForgotPasswordAsync(request, origin));
        }

        [HttpPost("reset-password")]
        [SwaggerOperation(
            Summary = "Reinicio de contrasenia",
            Description = "Permite al usuario cambiar su contrasenia actual por una nueva"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest request)
        {           
            return Ok(await _accountService.ResetPasswordAsync(request));
        }
    }
}
