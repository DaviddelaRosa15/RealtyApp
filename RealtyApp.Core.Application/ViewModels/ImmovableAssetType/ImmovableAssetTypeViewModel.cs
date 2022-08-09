using RealtyApp.Core.Application.ViewModels.ImmovableAsset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.ViewModels.ImmovableAssetType
{
    public class ImmovableAssetTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Navigation Properties
        public ICollection<ImmovableAssetViewModel> ImmovableAssets { get; set; }

    }
}
