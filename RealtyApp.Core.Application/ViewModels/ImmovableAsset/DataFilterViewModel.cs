using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.ViewModels.ImmovableAsset
{
    public class DataFilterViewModel
    {
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public int MaxBathroomQuantity { get; set; }
        public int MaxBedroomQuantity { get; set; }
    }
}
