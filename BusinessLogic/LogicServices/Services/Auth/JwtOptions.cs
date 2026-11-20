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
        /// Издатель токена (issuer).
        /// </summary>
        public string Issuer { get; set; } = "ToDoListWebAPI";

        /// <summary>
        /// Аудитория токена (потребитель токена).
        /// </summary>
        public string Audience { get; set; } = "ToDoListClient";

        /// <summary>
        /// Время жизни токена в часах.
        /// После истечения этого срока токен считается недействительным.
        /// </summary>
        public int ExpiresHours { get; set; }
    }
}
