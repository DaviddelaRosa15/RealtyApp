﻿using RealtyApp.Core.Application.ViewModels.FavoriteImmovable;
using RealtyApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Interfaces.Services
{
   public interface IFavoriteImmovableService:IGenericService<SaveFavoriteImmovableViewModel, FavoriteImmovableViewModel, FavoriteImmovable>
   {
   }
}
