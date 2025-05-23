using System.ComponentModel.DataAnnotations;

namespace DataAccess.Requests
{
    /// <summary>
    /// DTO на обновление существующей категории.
    /// </summary>
    public class CategoryUpdateRequest
    {
        /// <summary>
        /// Новое название категории.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = default!;
    }
}
