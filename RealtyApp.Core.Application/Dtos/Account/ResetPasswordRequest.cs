using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Dtos.Account
{
    /// <summary>
    /// Parámetros para el reinicio de contrasenia
    /// </summary>  
    public class ResetPasswordRequest
    {        
        [SwaggerParameter(Description = "El correo del usuario que se le quiere cambiar la contrasenia")]
        public string Email { get; set; }

        [SwaggerParameter(Description = "El token que permite poder reiniciar la contrasenia")]
        public string Token { get; set; }

        [SwaggerParameter(Description = "La nueva contrasenia del usuario")]
        public string Password { get; set; }

        [SwaggerParameter(Description = "La confirmacion de la nueva contrasenia del usuario")]
        public string ConfirmPassword { get; set; }
    }
}
