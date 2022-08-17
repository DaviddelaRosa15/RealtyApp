using AutoMapper;
using RealtyApp.Core.Application.Helpers;
using RealtyApp.Core.Application.Interfaces.Repositories;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Application.ViewModels.Improvement_Immovable;
using RealtyApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Services
{
    public class Improvement_ImmovableService : GenericService<SaveImprovement_ImmovableViewModel, Improvement_ImmovableViewModel, Improvement_Immovable>, IImprovement_ImmovableService
    {
        private readonly IMapper _mapper;
        private readonly IImprovement_ImmovableRepository _repository;
        public Improvement_ImmovableService(IImprovement_ImmovableRepository repository, IMapper mapper) : base(repository, mapper)
        {
            this._mapper = mapper;
            this._repository = repository;
        }

        public async Task<List<Improvement_ImmovableViewModel>> GetAllViewModelWithIncludes()
        {
            var improvements = await _repository.GetAllWithIncludeAsync(new List<string>() { });

            List<Improvement_ImmovableViewModel> improvementViews = _mapper.Map<List<Improvement_ImmovableViewModel>>(improvements);

            return improvementViews;
        }
        

    }
}
