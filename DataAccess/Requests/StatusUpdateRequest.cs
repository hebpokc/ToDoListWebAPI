using System.ComponentModel.DataAnnotations;

namespace DataAccess.Requests
{
    /// <summary>
    /// DTO на обновление существующего статуса.
    /// </summary>
    public class StatusUpdateRequest
    {
        /// <summary>
        /// Новое значение флага завершённости.
        /// </summary>
        [Required(ErrorMessage = "IsCompleted is required.")]
        public bool IsCompleted { get; set; }
    }
}
