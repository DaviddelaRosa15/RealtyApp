using RealtyApp.Core.Application.Dtos.EntitiesDTOs.ImmovableAsset;
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
        Task<List<ImmovableAssetViewModel>> GetAllViewModelWithFilters(FilterViewModel filters, string id);
        Task<List<ImmovableAssetViewModel>> GetAllViewModelWithIncludes();
        Task<DetailsViewModel> GetDetailsViewModel(int id);
        Task<int> CountImmovobleAsset();
        Task DeleteByIdAgent(string id);
        Task<DataFilterViewModel> GetDataFilterViewModel();
        Task<int> CountImmovableTypeById(int id);
        Task<int> CountSellTypeById(int id);
        Task<List<DetailsViewModelApi>> GetIncludeDetails();
        Task<DetailsViewModelApi> GetIncludeDetailsById(int id);
        Task<DetailsViewModelApi> GetIncludeDetailsByCode(string code);

    }
}
