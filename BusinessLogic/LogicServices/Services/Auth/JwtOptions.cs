namespace BusinessLogic.LogicServices.Services.Auth
{
    /// <summary>
    /// Предоставляет настройки, используемые для генерации и проверки JWT-токенов.
    /// </summary
    public class JwtOptions
    {
        /// <summary>
        /// Секретный ключ, используемый для подписи JWT-токена.
        /// Должен быть достаточно сложным и уникальным.
        /// </summary>
        public string SecretKey { get; set; } = default!;

        /// <summary>
        /// Время жизни токена в часах.
        /// После истечения этого срока токен считается недействительным.
        /// </summary>
        public int ExpiresHours { get; set; }
    }
}
