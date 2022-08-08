using AutoMapper;
using RealtyApp.Core.Application.Interfaces.Repositories;
using RealtyApp.Core.Application.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Services
{
    public class GenericService<SaveViewModel, ViewModel, Model> : IGenericService<SaveViewModel, ViewModel, Model>
        where SaveViewModel : class
        where ViewModel : class
        where Model : class
    {
        private readonly IGenericRepository<Model> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Model> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task Update(SaveViewModel vm, int id)
        {
            Model entity = _mapper.Map<Model>(vm);

            await _repository.UpdateAsync(entity, id);
        }

        public virtual async Task<SaveViewModel> Add(SaveViewModel vm)
        {
            Model entity = _mapper.Map<Model>(vm);

            entity = await _repository.AddAsync(entity);

            SaveViewModel categoryVm = _mapper.Map<SaveViewModel>(entity);
            return categoryVm;
        }

        public virtual async Task Delete(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(entity);
        }

        public virtual async Task<SaveViewModel> GetByIdSaveViewModel(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            SaveViewModel vm = _mapper.Map<SaveViewModel>(entity);
            return vm;
        }

        public virtual async Task<List<ViewModel>> GetAllViewModel()
        {
            var entityList = await _repository.GetAllAsync();

            return _mapper.Map<List<ViewModel>>(entityList);
        }

    }
}
