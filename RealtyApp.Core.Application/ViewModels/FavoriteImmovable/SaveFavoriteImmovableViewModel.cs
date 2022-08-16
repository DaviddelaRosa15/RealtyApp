using RealtyApp.Core.Application.ViewModels.ImmovableAsset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.ViewModels.FavoriteImmovable
{
    public class SaveFavoriteImmovableViewModel
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public int ImmovableAssetId { get; set; }
        public ImmovableAssetViewModel ImmovableAsset { get; set; }
    }
}
