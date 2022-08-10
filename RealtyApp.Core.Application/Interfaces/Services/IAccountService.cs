using RealtyApp.Core.Application.Dtos.Account;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsyncWebApi(AuthenticationRequest request);
        Task<AuthenticationResponse> AuthenticateAsyncWebApp(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<RegisterResponse> RegisterDeveloperUserAsync(RegisterRequest request);
        Task<RegisterResponse> RegisterAdministratorUserAsync(RegisterRequest request);
        Task<RegisterResponse> RegisterAgentUserAsync(RegisterRequest request);
        Task<RegisterResponse> RegisterClientUserAsync(RegisterRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task DeleteAsync(string id);
        Task UpdateAsync(string id);
        Task SignOutAsync();
    }
}