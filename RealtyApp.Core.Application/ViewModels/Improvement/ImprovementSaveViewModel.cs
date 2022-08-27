using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.ViewModels.Improvement
{
    public class ImprovementSaveViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe de especificar el un nombre para esta mejora.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe de especificar una descripción para esta mejora.")]
        public string Description { get; set; }

        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
    }
}
