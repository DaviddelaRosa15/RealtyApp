using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Dtos.Account
{
    /// <summary>
    /// Parámetros para la creacion de usuario basico
    /// </summary> 
    public class RegisterRequest
    {
        [SwaggerParameter(Description = "El nombre de el usuario")]
        [Required(ErrorMessage = "Debe de especificar un valor para el nombre.")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [SwaggerParameter(Description = "El apellido de el usuario")]
        [Required(ErrorMessage = "Debe de especificar un valor para el apellido.")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }


        [SwaggerParameter(Description = "Cedula o Documento de identificación ciudadana.")]
        [Required(ErrorMessage = "Debe de especificar un valor para el documento de identificación.")]
        [DataType(DataType.Text)]
        public string CardIdentification { get; set; }


        [SwaggerParameter(Description = "El correo de el usuario")]
        [Required(ErrorMessage = "Debe de especificar un valor para el correo electronico.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
       
        [SwaggerParameter(Description = "El nombre de usuario")]
        [Required(ErrorMessage = "Debe de especificar un valor para el nombre de usuario.")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [SwaggerParameter(Description = "La contrasenia del usuario")]
        [Required(ErrorMessage = "Debe de especificar un valor para la contraseña.")]
        public string Password { get; set; }

        [SwaggerParameter(Description = "La confirmacion de la contrasenia del usuario")]
        [Required(ErrorMessage = "Debe de especificar un valor para confirmar la contraseña.")]
        [Compare(nameof(Password), ErrorMessage = "Ambas contraseñas deben de coincidir.")]
        public string ConfirmPassword { get; set; }

        [SwaggerParameter(Description = "El telefono del usuario")]
        [Required(ErrorMessage = "Debe de especificar un número de teléfono activo.")]
        [DataType(DataType.Text)]
        public string Phone { get; set; }
    }
}
