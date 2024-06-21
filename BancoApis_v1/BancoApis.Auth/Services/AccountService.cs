using BancoApis.Auth.Helpers;
using BancoApis.Auth.Models;
using BancoApis.DomainModel.Auth;
using BancoApis.DomainServices.Abstractions.Services;
using BancoApis.DomainServices.Dtos.Requests;
using BancoApis.DomainServices.Dtos.Responses;
using BancoApis.DomainServices.Dtos.Users;
using BancoApis.Middlewares.Exceptions;
using BancoApis.Utilities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BancoApis.Auth.Services
{
    internal class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        public AccountService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<ResultResponse<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            var userAuthenticate = await _userManager.FindByEmailAsync(request.Email);
            if (userAuthenticate == null)
            {
                throw new KeyNotFoundException($"No hay una cuenta con el email {request.Email}.");
            }

            var result = await _signInManager.PasswordSignInAsync(userAuthenticate.UserName, request.Password, false, lockoutOnFailure:false);
            if (!result.Succeeded)
            {
                throw new ApiException("Las credenciales no son válidas.");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateJwTokenAsync(userAuthenticate);
            AuthenticationResponse response = new AuthenticationResponse
            {
                Id = userAuthenticate.Id,
                JwToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = userAuthenticate.Email,
                UserName = userAuthenticate.UserName,
            };

            var rolesList = await _userManager.GetRolesAsync(userAuthenticate).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.Isverified = userAuthenticate.EmailConfirmed;

            var refreshToken = GenerateRefreshToken(ipAddress);
            response.RefreshToken = refreshToken.Token;

            return new ResultResponse<AuthenticationResponse>($"Usuario {userAuthenticate.UserName} autenticado exitosamente.", response);
        }

        public async Task<ResultResponse<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var userWithSomeUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSomeUserName != null)
            {
                throw new ApiException($"El nombre de usuario {request.UserName} ya fue registrado previamente.");
            }

            var applicationUser = new ApplicationUser 
            { 
                Email = request.Email,
                Name = request.Name,
                UserName = request.UserName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var userWithSomeEmail = await _userManager.FindByNameAsync(request.Email);
            if (userWithSomeEmail != null)
            { 
                throw new ApiException($"El email {request.Email} ya fue registrado previamente.");
            }
            else
            {
                var result = await _userManager.CreateAsync(applicationUser, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(applicationUser, Roles.Basic.ToString());
                    return new ResultResponse<string>($"El usuario {request.UserName} fue registrado exitosamente.", applicationUser.Id);
                }
                else
                {
                    var errorsMessage = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        errorsMessage = error.Description;
                    }
                    throw new ApiException(errorsMessage);
                }
            }
        }

        private async Task<JwtSecurityToken> GenerateJwTokenAsync(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            string ipAddress  = IpHelper.GetIpAddress();
            var claim = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("ip", ipAddress),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claim,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
            ); ;

            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken(string ipAdddress)
        {
            RefreshToken refreshToken = new();
            string? randomTokenString = null;
            
            using (var rngCriptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[40];
                rngCriptoServiceProvider.GetBytes(randomBytes);
                randomTokenString = BitConverter.ToString(randomBytes).Replace("-", "");
            }

            if (randomTokenString is not null)
            {
                refreshToken.Token = randomTokenString;
                refreshToken.Expires = DateTime.Now.AddMinutes(30);
                refreshToken.Created = DateTime.Now;
                refreshToken.CreateByIp = ipAdddress;
            }

            return refreshToken;
        }
    }
}
