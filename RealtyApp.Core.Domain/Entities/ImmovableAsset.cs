using RealtyApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Domain.Entities
{
    public class ImmovableAsset : AuditableBaseEntity
    {
        public string Code { get; set; }
        public string IdUser { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string UrlImage01 { get; set; }
        public string UrlImage02 { get; set; }
        public string UrlImage03 { get; set; }
        public string UrlImage04 { get; set; }
        public double Meters { get; set; }
        public double BedroomQuantity { get; set; }
        public double BathroomQuantity { get; set; }

        //Navigation Properties
        public ImmovableAssetType ImmovableAssetType { get; set; }
        public int ImmovableAssetTypeId { get; set; }
        public SellType SellType { get; set; }
        public int SellTypeId { get; set; }
        public ICollection<FavoriteImmovable> FavoriteImmovables { get; set; }
        public ICollection<Improvement_Immovable> Improvement_Immovables { get; set; }
    }
}
