using RealtyApp.Core.Application.ViewModels.ImmovableAsset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.ViewModels.SellType
{
    public class SellTypeViewModel
    {
        public int Id { get; set; }
        public int CountUseType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ImmovableAssetViewModel> ImmovableAssets { get; set; }
    }
}
