using System.ComponentModel.DataAnnotations;

namespace DataAccess.Requests
{
    /// <summary>
    /// DTO для запроса входа пользователя в систему.
    /// Содержит обязательные поля: email и пароль.
    /// </summary>
    public class UserLoginRequest
    {
        /// <summary>
        /// Электронная почта пользователя, которая будет использоваться для входа.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        /// <summary>
        /// Пароль для учетной записи пользователя.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(24, ErrorMessage = "Password must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
