using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    /// <summary>
    /// Представляет напоминание об отправке уведомления по задаче.
    /// Используется для предотвращения повторной отправки.
    /// </summary>
    public class TaskReminder
    {
        /// <summary>
        /// Уникальный идентификатор записи.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Идентификатор задачи, к которой относится напоминание.
        /// </summary>
        public Guid TaskId { get; set; }

        /// <summary>
        /// Дата и время, когда уведомление было отправлено.
        /// </summary>
        public DateTime SentAt { get; set; }
        
        /// <summary>
        /// Задача, связанная с напоминанием
        /// </summary>
        [ForeignKey("TaskId")]
        public TaskEntity? Task { get; set; }
    }
}
