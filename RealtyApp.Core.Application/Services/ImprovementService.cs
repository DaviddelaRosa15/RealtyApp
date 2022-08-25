using AutoMapper;
using RealtyApp.Core.Application.Interfaces.Repositories;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Application.ViewModels.Improvement;
using RealtyApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Services
{
    public class ImprovementService : GenericService<ImprovementSaveViewModel, ImprovementViewModel, Improvement>, IImprovementService
    {
        private readonly IMapper _mapper;
        private readonly IImprovementRepository _repository;
        public ImprovementService(IImprovementRepository repository,
            IMapper mapper,
            IImprovement_ImmovableService improvement_ImmovableService) : base(repository, mapper)
        {
            this._mapper = mapper;
            this._repository = repository;
        }
        public async Task<List<ImprovementViewModel>> GetAllViewModelWithIncludes()
        {
            var improvements = await _repository.GetAllWithIncludeAsync(new List<string>() { "Improvement_Immovable" });

            List<ImprovementViewModel> improvementViews = _mapper.Map<List<ImprovementViewModel>>(improvements);

            return improvementViews;
        }
        
        
    }
}
