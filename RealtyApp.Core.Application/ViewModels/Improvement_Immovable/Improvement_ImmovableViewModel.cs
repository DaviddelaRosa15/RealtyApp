using RealtyApp.Core.Application.ViewModels.ImmovableAsset;
using RealtyApp.Core.Application.ViewModels.Improvement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.ViewModels.Improvement_Immovable
{
    public class Improvement_ImmovableViewModel
    {
        public int Id { get; set; }
        public ImprovementViewModel Improvement { get; set; }
        public int ImprovementId { get; set; }
        public ImmovableAssetViewModel ImmovableAsset { get; set; }
        public int ImmovableAssetId { get; set; }
    }
}
