using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    /// <summary>
    /// Представляет статус задачи.
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Уникальный идентификатор статуса.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Флаг, указывающий, является ли статус завершённым.
        /// Используется для определения выполненных задач.
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Список задач с этим статусом.
        /// </summary>
        public ICollection<TaskEntity>? Tasks { get; set; }
    }
}
