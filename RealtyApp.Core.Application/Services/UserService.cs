using AutoMapper;
using RealtyApp.Core.Application.Dtos.Account;
using RealtyApp.Core.Application.DTOs.Email;
using RealtyApp.Core.Application.Interfaces.Repositories;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Application.ViewModels.User;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UserService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse userResponse = await _accountService.AuthenticateAsyncWebApp(loginRequest);
            return userResponse;
        }
        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }        
        //public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin)
        //{
        //    RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            
        //    //Alex bro, tirarle un ojo al servicio AccountServices, puedes modificar los metodos como te convengan.
        //    //no le hice mucho solo lo deje definido con la plantilla de quejo david. Ya el BasicNoExiste.
            
        //    //return await _accountService.RegisterBasicUserAsync(registerRequest, origin);
        //    return new RegisterResponse();

        //}
        public async Task<RegisterResponse> RegisterAgentUserAsync(SaveUserViewModel vm)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterAgentUserAsync(registerRequest);
        }
        public async Task<RegisterResponse> RegisterClientUserAsync(SaveUserViewModel vm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterClientUserAsync(registerRequest,origin);
        }
        public async Task UpdateAsync(string id)
        {
            await _accountService.UpdateAsync(id);
        }
        public async Task DeleteAsync(string id)
        {
            await _accountService.DeleteAsync(id);
        }


        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin)
        {
            ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(vm);
            return await _accountService.ForgotPasswordAsync(forgotRequest, origin);
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm)
        {
            ResetPasswordRequest resetRequest = _mapper.Map<ResetPasswordRequest>(vm);
            return await _accountService.ResetPasswordAsync(resetRequest);
        }
    }
}
