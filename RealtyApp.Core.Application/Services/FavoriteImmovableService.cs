using AutoMapper;
using RealtyApp.Core.Application.Interfaces.Repositories;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Application.ViewModels.FavoriteImmovable;
using RealtyApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Services
{
    public class FavoriteImmovableService : GenericService<SaveFavoriteImmovableViewModel, FavoriteImmovableViewModel, FavoriteImmovable>, IFavoriteImmovableService
    {
        private readonly IFavoriteImmovableRepository _favoriteImmRepository;
        private readonly IMapper _mapper;

        public FavoriteImmovableService(IFavoriteImmovableRepository favoriteImmRepository, IMapper mapper)
        : base(favoriteImmRepository, mapper)
        {
            _favoriteImmRepository = favoriteImmRepository;
            _mapper = mapper;
        }
    }
}
