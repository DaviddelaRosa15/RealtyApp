using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Dtos.EntitiesDTOs.SellType
{
    public class SellTypeSaveDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe de especificar el un nombre para este tipo de venta.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe de especificar una descripción para este tipo de venta.")]
        public string Description { get; set; }
    }
}
