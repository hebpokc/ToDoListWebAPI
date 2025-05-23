using System.ComponentModel.DataAnnotations;

namespace DataAccess.Requests
{
    /// <summary>
    /// DTO на создание нового расхода.
    /// </summary>
    public class ExpenseCreateRequest
    {
        /// <summary>
        /// Сумма расхода.
        /// </summary>
        [Required(ErrorMessage = "Amount is required.")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Валюта расхода (например: RUB, USD).
        /// По умолчанию: RUB.
        /// </summary>
        public string Currency { get; set; } = "RUB";

        /// <summary>
        /// Дата и время совершения расхода.
        /// По умолчанию: текущее время UTC.
        /// </summary>
        public DateTime SpentAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Идентификатор связанной задачи.
        /// </summary>
        [Required(ErrorMessage = "TaskId is required.")]
        public Guid TaskId { get; set; }
    }
}
