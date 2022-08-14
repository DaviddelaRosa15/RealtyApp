using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.ViewModels.User
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public int CountImmovable { get; set; }
        public string LastName { get; set; }
        public string CardIdentification { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsVerified { get; set; }
    }
}
