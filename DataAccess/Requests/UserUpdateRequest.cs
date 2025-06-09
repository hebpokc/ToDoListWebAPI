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
    }
}
