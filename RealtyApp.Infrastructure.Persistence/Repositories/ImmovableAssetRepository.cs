using RealtyApp.Infrastructure.Persistence.Repository;
using RealtyApp.Core.Application.Interfaces.Repositories;
using RealtyApp.Core.Domain.Entities;
using RealtyApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RealtyApp.Infrastructure.Persistence.Repositories
{
  public  class ImmovableAssetRepository : GenericRepository<ImmovableAsset>, IImmovableAssetRepository
  {
        private readonly ApplicationContext _dbContext;

        public ImmovableAssetRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CountImmovobleAsset()
        {
          var countImmovable  =  await _dbContext.Set<ImmovableAsset>().ToListAsync();
          return countImmovable.Count;
        }
        public async Task DeleteByIdAgent(string id)
        {
            List<ImmovableAsset> immovableAssets = await _dbContext.Set<ImmovableAsset>().ToListAsync();
             _dbContext.RemoveRange(immovableAssets.Where(immo=> immo.AgentId==id));
            await _dbContext.SaveChangesAsync();
        }
        public async Task<int> CountImmovableTypeById(int id)
        {
            var countImmovable = await _dbContext.Set<ImmovableAsset>()
                .Where(imm_Improm => imm_Improm.ImmovableAssetTypeId == id)
                .ToListAsync();
            return countImmovable.Count;
        }
        public async Task<int> CountSellTypeById(int id)
        {
            var countImmovable = await _dbContext.Set<ImmovableAsset>()
                .Where(imm_Improm => imm_Improm.SellTypeId == id)
                .ToListAsync();
            return countImmovable.Count;
        }
    }
}
