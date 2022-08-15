using RealtyApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Interfaces.Repositories
{
   public interface IImmovableAssetRepository:IGenericRepository<ImmovableAsset>
   {
        Task<int> CountImmovobleAsset();
    }
}
