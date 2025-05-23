using System.ComponentModel.DataAnnotations;

namespace DataAccess.Requests
{
    /// <summary>
    /// DTO на создание новой категории.
    /// </summary>
    public class CategoryCreateRequest
    {
        /// <summary>
        /// Название категории.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = default!;
    }
}
