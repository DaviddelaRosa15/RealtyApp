using RealtyApp.Core.Application.ViewModels.ImmovableAsset;
using RealtyApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Interfaces.Services
{
   public interface IImmovableAssetService:IGenericService<SaveImmovableAssetViewModel,ImmovableAssetViewModel, ImmovableAsset>
    {
        Task<List<ImmovableAssetViewModel>> GetAllViewModelWithFilters(FilterViewModel filters);
        Task<List<ImmovableAssetViewModel>> GetAllViewModelWithIncludes();
    }
}
