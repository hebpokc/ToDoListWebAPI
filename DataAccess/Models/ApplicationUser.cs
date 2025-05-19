using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    /// <summary>
    /// Представляет пользователя приложения.
    /// </summary>
    public class ApplicationUser
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = default!;

        /// <summary>
        /// Email пользователя.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        /// <summary>
        /// Хэшированный пароль пользователя.
        /// </summary>
        [Required]
        public string PasswordHash { get; set; } = default!;

        /// <summary>
        /// Список задач, связанных с пользователем.
        /// </summary>
        public ICollection<TaskEntity>? Tasks { get; set; }
    }
}
