using RealtyApp.Core.Application.Dtos.Account;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.Agent;
using RealtyApp.Core.Application.Helpers;
using RealtyApp.Core.Application.ViewModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterAgentUser(SaveUserViewModel vm);
        Task<RegisterResponse> RegisterClientUser(SaveUserViewModel vm, string origin);
        Task<RegisterResponse> RegisterDeveloperUser(SaveUserViewModel vm);
        Task<RegisterResponse> RegisterAdministratorUser(SaveUserViewModel vm);
        Task SignOutAsync();
        Task<SaveUserViewModel> Update(SaveUserViewModel vm, string id);
        Task DeleteAsync(string id);
        Task<List<UserViewModel>> GetAllUsersAdmin();
        Task<List<UserViewModel>> GetAllUserAgentAsync();
        Task<List<UserViewModel>> GetAllUsersDeveloper();
        Task<SaveUserViewModel> GetUserById(string id);
        Task<List<UserViewModel>> GetUserAgentByName(string name);
        Task<AgentDTO> GetAgentById(string id);
        Task<List<AgentDTO>> GetAllAgents();
        Task<bool> ChangeUserStatus(string id, string status = null);
        Task<CountUser> CountClient();
        Task<CountUser> CountAgent();
        Task<CountUser> CountDeveloper();
        Task UpdateUserImageAsync(SaveUserViewModel vm);
    }
}