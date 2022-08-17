using RealtyApp.Core.Application.ViewModels.SellType;
using RealtyApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Interfaces.Services
{
    public interface ISellTypeService : IGenericService<SaveSellTypeViewModel, SellTypeViewModel, SellType>
    {
        //Custome Functionality
        public Task<List<SellTypeViewModel>> GetAllViewModelWithIncludes();
        Task<List<SellTypeViewModel>> GetAllWithSellType();

    }
}
