using RealtyApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Domain.Entities
{
    public class ImmovableAssetType : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        //Navigation Properties
        public ICollection<ImmovableAsset> ImmovableAssets { get; set; }
    }
}
