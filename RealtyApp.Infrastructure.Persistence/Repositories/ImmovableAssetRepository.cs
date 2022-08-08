using RealtyApp.Infrastructure.Persistence.Repository;
using RealtyApp.Core.Application.Interfaces.Repositories;
using RealtyApp.Core.Domain.Entities;
using RealtyApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Infrastructure.Persistence.Repositories
{
  public  class ImmovableAssetRepository : GenericRepository<ImmovableAsset>, IImmovableAssetRepository
  {
        private readonly ApplicationContext _dbContext;

        public ImmovableAssetRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
