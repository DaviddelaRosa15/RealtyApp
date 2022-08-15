using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.ViewModels.ImmovableAsset
{
   public class ImmovableAssetViewModel
   {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string UrlImage01 { get; set; }
        public string UrlImage02 { get; set; }
        public string UrlImage03 { get; set; }
        public string UrlImage04 { get; set; }
        public double Meters { get; set; }
        public int BedroomQuantity { get; set; }
        public int BathroomQuantity { get; set; }
        public string AgentId { get; set; }
        public int ImmovableAssetTypeId { get; set; }
        public string ImmovableAssetTypeName { get; set; }
        public int SellTypeId { get; set; }
        public string IdUser { get; set; }
        public string SellTypeName { get; set; }
    }
}
