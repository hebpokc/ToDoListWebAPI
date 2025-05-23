using System.ComponentModel.DataAnnotations;

namespace DataAccess.Requests
{
    /// <summary>
    /// DTO на обновление существующего расхода.
    /// </summary>
    public class ExpenseUpdateRequest
    {
        /// <summary>
        /// Новая сумма расхода.
        /// </summary>
        [Required(ErrorMessage = "Amount is required.")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Новая валюта расхода (например: RUB, USD).
        /// </summary>
        public string Currency { get; set; } = "RUB";

        /// <summary>
        /// Новая дата и время совершения расхода.
        /// По умолчанию: текущее время UTC.
        /// </summary>
        public DateTime SpentAt { get; set; } = DateTime.UtcNow;
    }
}
