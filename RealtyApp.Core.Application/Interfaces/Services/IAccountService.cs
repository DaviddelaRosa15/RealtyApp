using RealtyApp.Core.Application.Dtos.Account;
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
        Task UpdateAsync(RegisterRequest request, string id);
        Task SignOutAsync();
        Task<List<UserViewModel>> GetAllUserAdminAsync();
        Task<List<UserViewModel>> GetAllUserDeveloperAsync();
    }
}