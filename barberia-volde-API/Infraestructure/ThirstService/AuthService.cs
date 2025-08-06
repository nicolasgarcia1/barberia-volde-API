using Application.Interfaces;
using Domain.Interfaces;
using Application.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using Domain.Entities;
using Application.Models.Request;
using BCrypt.Net;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


namespace Infraestructure.ThirstService
{
    public class AuthService : IAuthService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly AuthServiceOptions _options;

        public AuthService(IAdminRepository adminRepository, IOptions<AuthServiceOptions> options)
        {
            _adminRepository = adminRepository;
            _options = options.Value;
        }

        private async Task<Admin?> ValidateAdmin(AuthRequest authRequest)
        {
            var admin = await _adminRepository.GetAdminAsync();
            if (admin != null && admin.Username == authRequest.Username && BCrypt.Net.BCrypt.Verify(authRequest.Password, admin.PasswordHash))
            {
                return admin;
            }
            return null;
        }

        public async Task<string> Authenticate(AuthRequest authRequest)
        {
            var admin = await ValidateAdmin(authRequest);

            if (admin == null)
            {
                throw new UnauthorizedAccessException("Usuario o contraseña inválidos.");
            }

            // Generate JWT token
            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));

            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("Id", admin.Id.ToString()));
            claimsForToken.Add(new Claim("Username", admin.Username));
            claimsForToken.Add(new Claim(ClaimTypes.Role, "Admin"));


            var jwtSecurityToken = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1), // Token expiration time,
                credentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return tokenToReturn.ToString();
        }
    }
}

