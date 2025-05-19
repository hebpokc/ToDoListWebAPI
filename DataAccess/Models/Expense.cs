using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class Expense
    {
        /// <summary>
        /// Уникальный идентификатор расходов.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Сумма расходов.
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// Валюта расходов (по умолчанию RUB).
        /// </summary>
        public string Currency { get; set; } = "RUB";

        /// <summary>
        /// Дата и время совершения расхода.
        /// </summary>
        public DateTime SpentAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Идентификатор связанной задачи.
        /// </summary>
        public Guid TaskId { get; set; }

        /// <summary>
        /// Связанная задача.
        /// </summary>
        [ForeignKey("TaskId")]
        public TaskEntity? Task { get; set; }
    }
}
