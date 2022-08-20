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
    public class ImprovementRepository : GenericRepository<Improvement>, IImprovementRepository
    {
        private readonly ApplicationContext _dbContext;
        public ImprovementRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<string>> GetImpromeByIdImmovable(int id)
        {
            List<Improvement_Immovable> improvements = await _dbContext.Set<Improvement_Immovable>().Where(x => x.ImprovementId == id).ToListAsync();
            List<string> Improms = new();
            foreach(Improvement_Immovable improvement in improvements)
            {
                var improveme = await GetByIdAsync(id);
                Improms.Add(improveme.Name);
            }
            return Improms;
        }
    }
}
