using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    /// <summary>
    /// Категория задачи (например: Работа, Личное и т.д.).
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Уникальный идентификатор категории.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Название категории.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = default!;

        /// <summary>
        /// Список задач, относящихся к этой категории.
        /// </summary>
        public ICollection<TaskEntity>? Tasks { get; set; }
    }

}
