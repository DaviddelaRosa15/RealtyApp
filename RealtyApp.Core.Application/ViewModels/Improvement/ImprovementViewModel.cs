using RealtyApp.Core.Application.ViewModels.Improvement_Immovable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.ViewModels.Improvement
{
    public class ImprovementViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        //Navigation Properties
        public ICollection<Improvement_ImmovableViewModel> Improvement_Immovables { get; set; }

    }
}
