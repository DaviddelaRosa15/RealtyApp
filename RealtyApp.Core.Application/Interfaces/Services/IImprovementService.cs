using RealtyApp.Core.Application.ViewModels.Improvement;
using RealtyApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Interfaces.Services
{
    public interface IImprovementService : IGenericService<ImprovementSaveViewModel, ImprovementViewModel, Improvement>
    {
        //Custome Functionality
        public Task<List<ImprovementViewModel>> GetAllViewModelWithIncludes();

    }
}
