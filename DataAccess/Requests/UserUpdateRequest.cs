using System.ComponentModel.DataAnnotations;

namespace DataAccess.Requests
{
    /// <summary>
    /// Запрос на обновление данных пользователя.
    /// </summary>
    public class UserUpdateRequest
    {
        /// <summary>
        /// Новое имя пользователя (логин).
        /// </summary>
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; } = default!;

        /// <summary>
        /// Новый адрес электронной почты пользователя.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = default!;

        /// <summary>
        /// Текущий пароль пользователя.
        /// </summary>
        [Required(ErrorMessage = "Текущий пароль обязателен для заполнения.")]
        public string CurrentPassword { get; set; } = default!;

        /// <summary>
        /// Новый пароль пользователя.
        /// </summary>
        [Required(ErrorMessage = "Новый пароль обязателен для заполнения.")]
        [MinLength(6, ErrorMessage = "Пароль должен содержать не менее 6 символов.")]
        public string NewPassword { get; set; } = default!;

    }
}
