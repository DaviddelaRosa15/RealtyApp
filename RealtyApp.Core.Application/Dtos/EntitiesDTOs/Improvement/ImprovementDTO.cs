using RealtyApp.Core.Application.Dtos.EntitiesDTOs.Improvement_Immovable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Dtos.EntitiesDTOs.Improvement
{
    public class ImprovementDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Navigation Properties
        public ICollection<Improvement_ImmovableDTO> Improvement_Immovables { get; set; }

    }
}
