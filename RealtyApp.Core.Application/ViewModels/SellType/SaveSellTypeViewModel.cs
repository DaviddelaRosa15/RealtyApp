using RealtyApp.Core.Application.ViewModels.ImmovableAsset;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.ViewModels.SellType
{
    public class SaveSellTypeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe de especificar un nombre para este tipo de venta.")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Debe de especificar un nombre para este tipo de venta.")]

        public string Description { get; set; }
        public List<ImmovableAssetViewModel> ImmovableAssets { get; set; }

        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
    }
}
