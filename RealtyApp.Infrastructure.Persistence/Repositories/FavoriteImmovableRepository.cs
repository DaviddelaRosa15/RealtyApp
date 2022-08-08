using Application.Repository;
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
   public class FavoriteImmovableRepository: GenericRepository<FavoriteImmovable>, IFavoriteImmovableRepository
    {
        private readonly ApplicationContext _dbContext;

        public FavoriteImmovableRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
