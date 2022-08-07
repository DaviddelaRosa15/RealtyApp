﻿using System.ComponentModel.DataAnnotations;

namespace RealtyApp.Core.Application.ViewModels.User
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Debe colocar el correo del usuario")]
        [DataType(DataType.Text)]
        public string Email { get; set; }    
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
