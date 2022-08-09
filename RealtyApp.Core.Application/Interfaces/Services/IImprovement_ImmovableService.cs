using RealtyApp.Core.Application.ViewModels.Improvement_Immovable;
using RealtyApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Interfaces.Services
{
    public interface IImprovement_ImmovableService : IGenericService<SaveImprovement_ImmovableViewModel, Improvement_ImmovableViewModel, Improvement_Immovable>
    {
        //Custome Functionality
        public Task<List<Improvement_ImmovableViewModel>> GetAllViewModelWithIncludes();

    }
}
