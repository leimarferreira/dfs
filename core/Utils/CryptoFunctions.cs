using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using core.Domain.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace core.Utils
{
    public class CryptoFunctions
    {
        public static string GenerateToken(IConfiguration configuration, User user)
        {
            var securityKey = Encoding.UTF8.GetBytes(configuration["SecurityKey"]);
            SymmetricSecurityKey key = new SymmetricSecurityKey(securityKey);

            var crendentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            int tokenExpireTimeLapse = int.Parse(configuration["TokenExpireTimeLapse"]);

            var token = new JwtSecurityToken(
                issuer: configuration["Issuer"],
                audience: configuration["Audience"],
                expires: DateTime.Now.AddMinutes(tokenExpireTimeLapse),
                signingCredentials: crendentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
