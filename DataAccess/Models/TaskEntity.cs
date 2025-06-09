using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    /// <summary>
    /// Задача в списке дел (To-Do).
    /// </summary>
    public class TaskEntity
    {
        /// <summary>
        /// Уникальный идентификатор задачи.
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Заголовок задачи.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = default!;

        /// <summary>
        /// Описание задачи.
        /// </summary>
        public string Description { get; set; } = default!;

        // <summary>
        /// Дата и время завершения задачи.
        /// </summary>
        public DateTime DueDate { get; set; }

        // Foreign keys

        /// <summary>
        /// Идентификатор пользователя, которому принадлежит задача.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Идентификатор категории задачи.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Идентификатор статуса задачи.
        /// </summary>
        public Guid StatusId { get; set; }

        // Navigation

        /// <summary>
        /// Пользователь, связанный с задачей.
        /// </summary>
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        /// <summary>
        /// Категория, к которой относится задача.
        /// </summary>
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        /// <summary>
        /// Текущий статус задачи.
        /// </summary>
        [ForeignKey("StatusId")]
        public Status? Status { get; set; }

        /// <summary>
        /// Информация о расходах по задаче (если применимо).
        /// </summary>
        public Expense? Expense { get; set; }
    }

}
