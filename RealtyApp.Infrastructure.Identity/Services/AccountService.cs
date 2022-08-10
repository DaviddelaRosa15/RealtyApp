using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealtyApp.Core.Application.Dtos.Account;
using RealtyApp.Core.Application.Enums;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Domain.Settings;
using RealtyApp.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using RealtyApp.Core.Application.DTOs.Email;

namespace RealtyApp.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly JWTSettings _jwtSettings;

        public AccountService(
              UserManager<ApplicationUser> userManager,
              SignInManager<ApplicationUser> signInManager,
              IEmailService emailService,
              IOptions<JWTSettings> jwtSettings
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _jwtSettings = jwtSettings.Value;
        }

        #region Authenticate
        public async Task<AuthenticationResponse> AuthenticateAsyncWebApi(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No existen cuentas registradas con el correo: {request.Email}, si el problema persiste haga contacto con nuestro soporte técnico";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Credenciales incorrectas para el correo: {request.Email}, si el problema persiste haga contacto con nuestro soporte técnico";
                return response;
            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Su cuenta no está activa, haga contacto con nuestro soporte técnico con";
                return response;
            }

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            if (!rolesList.Any(x => x.Equals(Roles.Administrator.ToString()) || x.Equals(Roles.Developer.ToString())))
            {
                response.HasError = true;
                response.Error = $"Usted no tiene permisos para usar la Api de RealtyApp";
                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;

            return response;
        }

        public async Task<AuthenticationResponse> AuthenticateAsyncWebApp(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByEmailAsync(request.Email);
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No existe una cuenta registrada con el email {request.Email}, si el problema persiste haga contacto con nuestro soporte técnico";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Credenciales incorrectas para el email {request.Email}, si el problema persiste haga contacto con nuestro soporte técnico";
                return response;
            }
            if (!user.EmailConfirmed)
            {
                if (rolesList.Any(x => x.Equals(Roles.Agent.ToString())))
                {
                    response.HasError = true;
                    response.Error = $"Su cuenta no está activa, haga contacto con nuestro soporte técnico";
                }
                else
                {
                    response.HasError = true;
                    response.Error = $"Su cuenta no está activa, dirijase a su bandeja de correo, busque el correo que le mandamos y active su cuenta. ";
                }

                return response;
            }

            if (rolesList.Any(x => x.Equals(Roles.Developer.ToString())))
            {
                response.HasError = true;
                response.Error = $"Usted no tiene permisos para usar la App de RealtyApp";
                return response;
            }

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.UrlImage = user.UrlImage;

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;

            return response;
        }

        #endregion

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task UpdateAsync(RegisterRequest request, string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UserName = request.UserName;
            user.CardIdentification = request.CardIdentification;
            user.PhoneNumber = request.Phone;
            user.UrlImage = request.ImageUrl;

            await _userManager.UpdateAsync(user);
        }
        public async Task DeleteAsync(string id)
        {
            ApplicationUser applicationUser = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(applicationUser);
        }

        #region Registration

        private async Task<RegisterResponse> ValidateUserBeforeRegistrationAsync(RegisterRequest request)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"username '{request.UserName}' is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' is already registered.";
                return response;
            }

            return response;

        }

        public async Task<RegisterResponse> RegisterDeveloperUserAsync(RegisterRequest request)
        {
            RegisterResponse response = await ValidateUserBeforeRegistrationAsync(request);

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                CardIdentification = request.CardIdentification,
                PhoneNumber = request.Phone,
                UrlImage = "/Images/User/placeholder_profile_image.png",
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Developer.ToString());
            }
            else
            {
                response.HasError = true;
                response.Error = $"An error occurred trying to register the user. ";
                response.Error += result.Errors.ToList()[0].Description;
                return response;
            }

            return response;
        }

        public async Task<RegisterResponse> RegisterAdministratorUserAsync(RegisterRequest request)
        {
            RegisterResponse response = await ValidateUserBeforeRegistrationAsync(request);

            //We could use AutoMaper here!
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                CardIdentification = request.CardIdentification,
                PhoneNumber = request.Phone,
                UrlImage = "/Images/User/placeholder_profile_image.png",
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Administrator.ToString());
            }
            else
            {
                response.HasError = true;
                response.Error = $"An error occurred trying to register the user. ";
                response.Error += result.Errors.ToList()[0].Description;
                return response;
            }

            return response;
        }

        public async Task<RegisterResponse> RegisterAgentUserAsync(RegisterRequest request)
        {
            RegisterResponse response = await ValidateUserBeforeRegistrationAsync(request);
            if (response.HasError)
            {
                return response;
            }

            //We could use AutoMaper here!
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                CardIdentification = request.CardIdentification,
                PhoneNumber = request.Phone,
                EmailConfirmed = false,
                UrlImage = request.ImageUrl
            };
            //Remember the image property
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Agent.ToString());
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    response.Error += $"{error.Description}";
                }
                response.HasError = true;
                return response;
            }
            response.Id = user.Id;
            return response;
        }

        public async Task<RegisterResponse> RegisterClientUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = await ValidateUserBeforeRegistrationAsync(request);
            if (response.HasError)
            {
                return response;
            }

            //We could use AutoMaper here!
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                CardIdentification = request.CardIdentification,
                PhoneNumber = request.Phone,
                UrlImage = request.ImageUrl
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Client.ToString());
                var verificationUri = await SendVerificationEmailUri(user, origin);
                await _emailService.SendAsync(new EmailRequest()
                {
                    To = user.Email,
                    Body = MakeEmailForConfirm(verificationUri),
                    Subject = "Confirm registration"
                });
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    response.Error += $"{error.Description}";
                }
                response.HasError = true;
                return response;
            }
            response.Id = user.Id;
            return response;
        }

        #endregion

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No existe cuenta registrada con este usuario";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Cuenta confirmada para {user.Email}. Ahora puedes usar la app";
            }
            else
            {
                return $"Ocurrió un error mientras se confirmaba la cuenta para el correo: {user.Email}.";
            }
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Email}";
                return response;
            }

            var verificationUri = await SendForgotPasswordUri(user, origin);

            await _emailService.SendAsync(new EmailRequest()
            {
                To = user.Email,
                Body = $"Please reset your account visiting this URL {verificationUri}",
                Subject = "reset password"
            });


            return response;
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Email}";
                return response;
            }

            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred while reset password";
                return response;
            }

            return response;
        }

        #region PrivateMethods
        private string MakeEmailForConfirm(string verificationUri)
        {
            string html = $"Please confirm your account visiting this URL {verificationUri}";
            return html;
        }

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmectricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredetials = new SigningCredentials(symmectricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredetials);

            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var ramdomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(ramdomBytes);

            return BitConverter.ToString(ramdomBytes).Replace("-", "");
        }


        private async Task<string> SendVerificationEmailUri(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);

            return verificationUri;
        }
        private async Task<string> SendForgotPasswordUri(ApplicationUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ResetPassword";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);

            return verificationUri;
        }

        #endregion
    }


}
