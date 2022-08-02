using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key; // 1 key to encrypt and decrypt token
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>{
            new Claim (JwtRegisteredClaimNames.NameId, user.UserName) // when user claims they are who they say they are
           };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), //
                Expires = DateTime.Now.AddDays(7),    // creating token
                SigningCredentials = creds            //
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDiscriptor);
            return tokenHandler.WriteToken(token);




























        }
    }
}