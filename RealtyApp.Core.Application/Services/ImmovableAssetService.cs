using AutoMapper;
using RealtyApp.Core.Application.Interfaces.Repositories;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Application.ViewModels.ImmovableAsset;
using RealtyApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Services
{
   public class ImmovableAssetService : GenericService<SaveImmovableAssetViewModel, ImmovableAssetViewModel, ImmovableAsset>, IImmovableAssetService
    {
        private readonly IImmovableAssetRepository _immovableAssetRepository;
        private readonly IMapper _mapper;

        public ImmovableAssetService(IImmovableAssetRepository ImmovableAssetRepository, IMapper mapper)
        : base(ImmovableAssetRepository, mapper)
        {
            _immovableAssetRepository = ImmovableAssetRepository;
            _mapper = mapper;
        }

        public async Task<int> CountImmovobleAsset()
        {
            return await _immovableAssetRepository.CountImmovobleAsset();
        }

    }
}
