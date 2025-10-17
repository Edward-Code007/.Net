using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using tests.Config;
using tests.DTOs;
using tests.Models;
using tests.Services.IServices;

namespace tests.Services
{
    public class JwtService(IOptions<JwtOption> options) : IJwtService
    {
        private JwtOption _options = options.Value;
        
        public string CreateToken(UserModel user, List<RoleModel> roles)
        {

            

            //Declarar Claims, o sea el contenido del payload del jwt
            var claims = new List<Claim> {
                new (ClaimTypes.Email, user.Email),
                
            };
            foreach (var role in roles) { 
                claims.Add(new(ClaimTypes.Role,role.Name));
            }

            // Declarar  la key correspondiente
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

            //en este paso se declara el metodo de cifrado

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            //Creacion del token en si
            var token_desc = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: credentials);

            //Serializar el token a jwt
            return new JwtSecurityTokenHandler().WriteToken(token_desc);
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

    }
}
