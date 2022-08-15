using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Dtos.EntitiesDTOs.Improvement
{
    public class ImprovementSaveDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe de especificar el un nombre para esta mejora.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe de especificar una descripción para esta mejora.")]
        public string Description { get; set; }
    }
}
