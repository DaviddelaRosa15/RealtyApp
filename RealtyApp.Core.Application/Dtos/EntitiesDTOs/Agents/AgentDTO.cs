using RealtyApp.Core.Application.Dtos.EntitiesDTOs.Improvement_Immovable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Dtos.EntitiesDTOs.Agent
{
    public class AgentDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PropertiesQuantity { get; set; }
        public string Email { get; set; }
        public string CardIdentification { get; set; }
        public string Phone { get; set; }
    }
}
