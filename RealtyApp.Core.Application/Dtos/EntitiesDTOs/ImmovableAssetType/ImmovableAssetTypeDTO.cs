using RealtyApp.Core.Application.Dtos.EntitiesDTOs.Improvement_Immovable;
using RealtyApp.Core.Application.ViewModels.ImmovableAsset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace RealtyApp.Core.Application.Dtos.EntitiesDTOs.ImmovableAssetType
{
    public class ImmovableAssetTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Navigation Properties
        public ICollection<Improvement_ImmovableDTO> ImmovableAssets { get; set; }

    }
}
