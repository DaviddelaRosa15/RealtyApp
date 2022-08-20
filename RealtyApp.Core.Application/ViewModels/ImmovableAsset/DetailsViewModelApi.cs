using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.ViewModels.ImmovableAsset
{
   public class DetailsViewModelApi
    {
        public int Id { get; set; }
        public int IdImprovement { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Meters { get; set; }
        public int BedroomQuantity { get; set; }
        public int BathroomQuantity { get; set; }
        public string AgentId { get; set; }
        public string ImmovableAssetTypeName { get; set; }
        public string SellTypeName { get; set; }
        public List<string> ImprovementNames { get; set; }
    }
}
