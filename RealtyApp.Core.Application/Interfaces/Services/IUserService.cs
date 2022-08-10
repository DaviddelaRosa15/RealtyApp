using RealtyApp.Core.Application.Dtos.Account;
using RealtyApp.Core.Application.ViewModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterAgentUserAsync(SaveUserViewModel vm);
        Task<RegisterResponse> RegisterClientUserAsync(SaveUserViewModel vm, string origin);
        Task SignOutAsync();
        Task UpdateAsync(SaveUserViewModel vm, string id);
        Task DeleteAsync(string id);
        Task<List<UserViewModel>> GetAllUsersAdmin();
        Task<List<UserViewModel>> GetAllUsersDeveloper();
        Task<List<UserViewModel>> GetUserById();
        Task ChangeUserStatus(string id);
    }
}