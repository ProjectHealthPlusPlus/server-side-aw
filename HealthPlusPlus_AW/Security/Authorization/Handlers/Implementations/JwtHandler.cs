﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using HealthPlusPlus_AW.Security.Authorization.Handlers.Interface;
using HealthPlusPlus_AW.Security.Authorization.Settings;
using HealthPlusPlus_AW.Security.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace HealthPlusPlus_AW.Security.Authorization.Handlers.Implementations
{
    public class JwtHandler : IJwtHandler
    {
        private readonly AppSettings _appSettings;

        public JwtHandler(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }


        public string GenerateToken(UserSec user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] {new Claim("id", user.Id.ToString())}),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken((tokenDescriptor));
            return tokenHandler.WriteToken(token);

        }

        public int? ValidateToken(string token)
        {
            if (token == null)
                return null;
 
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,

                        ClockSkew = TimeSpan.Zero
                    },
                    out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken) validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(
                    claim => claim.Type == "id").Value);

                return userId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}