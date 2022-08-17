using Microsoft.EntityFrameworkCore;
using RealtyApp.Core.Application.Interfaces.Repositories;
using RealtyApp.Core.Domain.Entities;
using RealtyApp.Infrastructure.Persistence.Contexts;
using RealtyApp.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Infrastructure.Persistence.Repositories
{
    public class Improvement_ImmovableRepository : GenericRepository<Improvement_Immovable>, IImprovement_ImmovableRepository
    {
        private readonly ApplicationContext _dbContext;
        public Improvement_ImmovableRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        
    }
}
