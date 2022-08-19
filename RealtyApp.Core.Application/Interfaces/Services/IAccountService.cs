using RealtyApp.Core.Application.Dtos.Account;
using RealtyApp.Core.Application.Helpers;
using RealtyApp.Core.Application.ViewModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsyncWebApi(AuthenticationRequest request);
        Task<AuthenticationResponse> AuthenticateAsyncWebApp(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<RegisterResponse> RegisterDeveloperUserAsync(RegisterRequest request);
        Task<RegisterResponse> RegisterAdministratorUserAsync(RegisterRequest request);
        Task<RegisterResponse> RegisterAgentUserAsync(RegisterRequest request);
        Task<RegisterResponse> RegisterClientUserAsync(RegisterRequest request, string origin);
        Task DeleteAsync(string id);
        Task<SaveUserViewModel> UpdateAsync(SaveUserViewModel vm);
        Task SignOutAsync();
        Task<List<UserViewModel>> GetAllUserAdminAsync();
        Task<List<UserViewModel>> GetAllUserAgentAsync();
        Task<List<UserViewModel>> GetAllUserDeveloperAsync();
        Task<SaveUserViewModel> GetUserByIdAsync(string id);
        Task<List<UserViewModel>> GetUserAgentByNameAsync(string name);
        Task<bool> ChangeUserStatusAsync(string id, string status = null);
        Task<CountUser> CountClient();
        Task<CountUser> CountAgent();
        Task<CountUser> CountDeveloper();
        Task UpdateUserImageAsync(SaveUserViewModel vm);

    }
}