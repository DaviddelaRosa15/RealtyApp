using RealtyApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Domain.Entities
{
    public class Improvement : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }       

        //Navigation Properties
        public ICollection<Improvement_Immovable> Improvement_Immovables { get; set; }
    }
}
