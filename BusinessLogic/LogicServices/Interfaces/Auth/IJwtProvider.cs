using DataAccess.Models;

namespace BusinessLogic.LogicServices.Interfaces.Auth
{
    /// <summary>
    /// Определяет контракт для генерации JWT-токена на основе данных пользователя.
    /// </summary>
    public interface IJwtProvider
    {
        /// <summary>
        /// Генерирует JWT-токен для указанного пользователя.
        /// </summary>
        /// <param name="user">Пользователь, для которого создаётся токен.</param>
        /// <returns>Сгенерированный JWT-токен в виде строки.</returns>
        public string GenerateToken(ApplicationUser user);
    }
}
