using BusinessLogic.LogicServices.Interfaces.Auth;
using DataAccess.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogic.LogicServices.Services.Auth
{
    /// <summary>
    /// Предоставляет функциональность для генерации JWT-токенов на основе данных пользователя.
    /// </summary>
    public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
    {
        private readonly JwtOptions _options = options.Value;

        /// <summary>
        /// Генерирует JWT-токен для указанного пользователя.
        /// </summary>
        /// <param name="user">Пользователь, для которого создаётся токен.</param>
        /// <returns>Сгенерированный JWT-токен в виде строки.</returns>
        public string GenerateToken(ApplicationUser user)
        {
            Claim[] claims = [new("userId", user.Id.ToString())];

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)), 
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_options.ExpiresHours));

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
