using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Features.SellType.Commands.UpdateSellType
{
    public class UpdateSellTypeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public string Description { get; set; }
    }
}
