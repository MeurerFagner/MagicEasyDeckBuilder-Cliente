using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MagicEasyDeckBuilderAPI.API.Configuration
{
    public class TokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(Usuario usuario)
        {
            var secret = _config.GetSection("AutenticacaoSegredo").Value;
            var key = Encoding.ASCII.GetBytes(secret);

            var tokeHandler = new JwtSecurityTokenHandler();

            var tokeDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name,usuario.Nome),
                    new Claim(ClaimTypes.Email,usuario.Email)
                }),
                Expires = DateTime.Now.AddDays(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokeHandler.CreateToken(tokeDescriptor);

            return tokeHandler.WriteToken(token);

        }
    }
}
