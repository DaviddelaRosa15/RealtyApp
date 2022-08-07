using RealtyApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Domain.Entities
{
    public class FavoriteImmovable : AuditableBaseEntity
    {
        public string ClientId { get; set; }

        //Navigation Properties
        public ImmovableAsset ImmovableAsset { get; set; }
        public int ImmovableAssetId { get; set; }
    }
}
