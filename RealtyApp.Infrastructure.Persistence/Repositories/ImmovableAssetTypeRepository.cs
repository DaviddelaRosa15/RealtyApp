using RealtyApp.Core.Application.Interfaces.Repositories;
using RealtyApp.Core.Domain.Entities;
using RealtyApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Infrastructure.Persistence.Repository
{
    public class ImmovableAssetTypeRepository : GenericRepository<ImmovableAssetType>
    {
        private readonly ApplicationContext _dbContext;
        public ImmovableAssetTypeRepository(ApplicationContext dbContext) 
            : base(dbContext)
        {
            this._dbContext = dbContext;
        }


    }
}
