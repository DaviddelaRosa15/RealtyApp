using RealtyApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Domain.Entities
{
    public class Improvement_Immovable : AuditableBaseEntity
    {
        //Navigation Properties
        public Improvement Improvement { get; set; }
        public int ImprovementId { get; set; }
        public ImmovableAsset ImmovableAsset { get; set; }
        public int ImmovableAssetId { get; set; }
    }
}
