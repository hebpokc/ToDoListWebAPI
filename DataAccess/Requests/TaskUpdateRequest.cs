using System.ComponentModel.DataAnnotations;

namespace DataAccess.Requests
{
    /// <summary>
    /// DTO на обновление существующей задачи.
    /// </summary>
    public class TaskUpdateRequest
    {
        /// <summary>
        /// Новый заголовок задачи.
        /// </summary>
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(200, ErrorMessage = "Title must be under 200 characters.")]
        public string Title { get; set; } = default!;

        /// <summary>
        /// Новое описание задачи.
        /// </summary>
        public string Description { get; set; } = default!;

        /// <summary>
        /// Новая дата завершения задачи.
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Новый идентификатор категории задачи.
        /// </summary>
        [Required(ErrorMessage = "CategoryId is required.")]
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Новый идентификатор статуса задачи.
        /// </summary>
        [Required(ErrorMessage = "StatusId is required.")]
        public Guid StatusId { get; set; }
    }
}
