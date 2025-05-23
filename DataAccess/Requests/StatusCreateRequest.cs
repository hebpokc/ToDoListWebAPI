using System.ComponentModel.DataAnnotations;

namespace DataAccess.Requests
{
    /// <summary>
    /// DTO на создание нового статуса.
    /// </summary>
    public class StatusCreateRequest
    {
        /// <summary>
        /// Флаг, указывающий, является ли статус завершённым.
        /// </summary>
        [Required(ErrorMessage = "IsCompleted is required.")]
        public bool IsCompleted { get; set; }
    }
}
