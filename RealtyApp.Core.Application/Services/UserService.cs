using AutoMapper;
using RealtyApp.Core.Application.Dtos.Account;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.Agent;
using RealtyApp.Core.Application.DTOs.Email;
using RealtyApp.Core.Application.Helpers;
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


        #region Registers
        public async Task<RegisterResponse> RegisterAgentUser(SaveUserViewModel vm)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterAgentUserAsync(registerRequest);
        }

        public async Task<RegisterResponse> RegisterClientUser(SaveUserViewModel vm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterClientUserAsync(registerRequest, origin);
        }

        public async Task<RegisterResponse> RegisterDeveloperUser(SaveUserViewModel vm)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterDeveloperUserAsync(registerRequest);
        }

        public async Task<RegisterResponse> RegisterAdministratorUser(SaveUserViewModel vm)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterAdministratorUserAsync(registerRequest);
        }
        #endregion

        #region ManageUsers
        public async Task<SaveUserViewModel> Update(SaveUserViewModel vm, string id)
        {
            vm.Id = id;
            return await _accountService.UpdateAsync(vm);
        }

        public async Task<SaveUserViewModel> UpdateAgentAsync(SaveUserViewModel vm, string id)
        {
            vm.Id = id;
            return await _accountService.UpdateAgentAsync(vm);
        }

        public async Task UpdateUserImageAsync(SaveUserViewModel vm)
        {
            await _accountService.UpdateUserImageAsync(vm);
        }
        public async Task DeleteAsync(string id)
        {
            await _accountService.DeleteAsync(id);
        }
        #endregion

        #region Gets
        public async Task<List<UserViewModel>> GetAllUsersAdmin()
        {
            return await _accountService.GetAllUserAdminAsync();
        }
        public async Task<List<UserViewModel>> GetAllUserAgentAsync()
        {
            return await _accountService.GetAllUserAgentAsync();
        }

        public async Task<List<UserViewModel>> GetAllUsersDeveloper()
        {
            return await _accountService.GetAllUserDeveloperAsync();
        }

        public async Task<SaveUserViewModel> GetUserById(string id)
        {
            return await _accountService.GetUserByIdAsync(id);
        }

        public async Task<List<UserViewModel>> GetUserAgentByName(string name)
        {
            return await _accountService.GetUserAgentByNameAsync(name);
        }

        public async Task<AgentDTO> GetAgentById(string id)
        {
            var user = await GetUserById(id);
            if(user != null)
            {
                AgentDTO agent = new()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    CardIdentification = user.CardIdentification,
                    Email = user.Email,
                    Phone = user.Phone
                };

                return agent;
            }

            return null;
        }

        public async Task<List<AgentDTO>> GetAllAgents()
        {
            var users = await GetAllUserAgentAsync();
            List<AgentDTO> agents = new();

            if (users != null)
            {
                foreach (var user in users)
                {

                    agents.Add(new AgentDTO()
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        CardIdentification = user.CardIdentification,
                        Email = user.Email,
                        Phone = user.Phone
                    });
                }
                return agents;
            }

            return null;
        }
        #endregion

        #region ManageStatus
        public async Task<bool> ChangeUserStatus(string id, string status = null)
        {
            var operationStatus = await _accountService.ChangeUserStatusAsync(id, status);
            return operationStatus;
        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }
        #endregion

        #region Counts
        public async Task<CountUser> CountClient()
        {
            return await _accountService.CountClient();
        }

        public async Task<CountUser> CountAgent()
        {
            return await _accountService.CountAgent();
        }

        public async Task<CountUser> CountDeveloper()
        {
            return await _accountService.CountDeveloper();
        }
        #endregion
    }
}
