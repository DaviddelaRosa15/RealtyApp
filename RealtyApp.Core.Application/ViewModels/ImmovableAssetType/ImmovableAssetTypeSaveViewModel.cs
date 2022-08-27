using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.ViewModels.ImmovableAssetType
{
    public class ImmovableAssetTypeSaveViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe de especificar un nombre para este tipo de inmueble.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Debe de especificar una descripción para este tipo de inmueble.")]
        public string Description { get; set; }

        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }

    }
}
