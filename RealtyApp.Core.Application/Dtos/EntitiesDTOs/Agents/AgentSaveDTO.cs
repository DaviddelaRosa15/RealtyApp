using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Dtos.EntitiesDTOs.Agent
{
    public class AgentSaveDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Debe de especificar el un nombre para este agente.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe de especificar el apellido para este agente.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe de especificar el correo para este agente.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe de especificar la cedula para este agente.")]
        public string CardIdentification { get; set; }

        [Required(ErrorMessage = "Debe de especificar el telefono para este agente.")]
        public string Phone { get; set; }
    }
}
