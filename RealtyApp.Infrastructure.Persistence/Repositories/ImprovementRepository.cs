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
        private readonly IImprovement_ImmovableRepository _immovabl_Improvement;
        public ImprovementRepository(ApplicationContext dbContext, IImprovement_ImmovableRepository repository) : base(dbContext)
        {
            _dbContext = dbContext;
            _immovabl_Improvement = repository;
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

        public override async Task DeleteAsync(Improvement entity)
        {

            var immovable_Improvements =await _immovabl_Improvement.GetAllAsync();
            var Improvements = immovable_Improvements.Where(x => x.ImprovementId == entity.Id);
            foreach(var improvement in Improvements)
            {
                await _immovabl_Improvement.DeleteAsync(improvement);
            }
            await base.DeleteAsync(entity);
        }



    }
}
