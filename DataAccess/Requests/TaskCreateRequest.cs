using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataAccess.Requests
{
    /// <summary>
    /// DTO на создание новой задачи.
    /// </summary>
    public class TaskCreateRequest
    {
        /// <summary>
        /// Заголовок задачи.
        /// </summary>
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(200, ErrorMessage = "Title must be under 200 characters.")]
        public string Title { get; set; } = default!;

        /// <summary>
        /// Описание задачи.
        /// </summary>
        public string Description { get; set; } = default!;

        /// <summary>
        /// Дата завершения задачи.
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Идентификатор категории задачи.
        /// </summary>
        [Required(ErrorMessage = "CategoryId is required.")]
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Идентификатор статуса задачи.
        /// </summary>
        [Required(ErrorMessage = "StatusId is required.")]
        public Guid StatusId { get; set; }

        /// <summary>
        /// Идентификатор пользователя, которому принадлежит задача.
        /// </summary>
        [Required(ErrorMessage = "UserId is required.")]
        public Guid UserId { get; set; }
    }
}
