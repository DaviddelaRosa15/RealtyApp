﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.ViewModels.ImmovableAsset
{
    public class FilterViewModel
    {
        public string Code { get; set; }
        public int? ImmovableAssetTypeId { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public int? BathroomQuantity { get; set; }
        public int? BedroomQuantity { get; set; }
    }
}
