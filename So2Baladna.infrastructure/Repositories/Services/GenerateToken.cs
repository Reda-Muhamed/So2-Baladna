using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using So2Baladna.Core.Entities;
using So2Baladna.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Infrastructure.Repositories.Services
{
    public class GenerateToken : IGenerateToken
    {
        private readonly IConfiguration configuration;

        public GenerateToken(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GetAndCreateToken(AppUser appUser)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name , appUser.UserName!),
                new Claim(ClaimTypes.Email , appUser.Email!),


            };
            var security = configuration["Token:Secret"];
            var key = Encoding.ASCII.GetBytes(security!);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = configuration["Token:Issuer"],
                SigningCredentials = credentials,
                NotBefore = DateTime.UtcNow

            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
            
        }
    }
}
