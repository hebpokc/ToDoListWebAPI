using DataAccess.Models;

namespace DataAccess.Responses
{
    /// <summary>
    /// DTO для ответа с данными пользователя (без хэша пароля).
    /// </summary>
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
    }

    /// <summary>
    /// DTO для ответа с данными задачи.
    /// </summary>
    public class TaskResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime DueDate { get; set; }
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid StatusId { get; set; }
        public string? CategoryName { get; set; }
        public bool IsCompleted { get; set; }
    }

    /// <summary>
    /// DTO для ответа с данными категории.
    /// </summary>
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }

    /// <summary>
    /// DTO для ответа с данными расхода.
    /// </summary>
    public class ExpenseResponse
    {
        public Guid Id { get; set; }
        public decimal? Amount { get; set; }
        public string Currency { get; set; } = default!;
        public DateTime SpentAt { get; set; }
        public Guid TaskId { get; set; }
    }

    /// <summary>
    /// DTO для ответа с данными статуса.
    /// </summary>
    public class StatusResponse
    {
        public Guid Id { get; set; }
        public bool IsCompleted { get; set; }
    }

    /// <summary>
    /// Методы расширения для маппинга сущностей в DTO-ответы.
    /// </summary>
    public static class ResponseMappings
    {
        public static UserResponse ToResponse(this ApplicationUser user) =>
            new()
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };

        public static TaskResponse ToResponse(this TaskEntity task) =>
            new()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                UserId = task.UserId,
                CategoryId = task.CategoryId,
                StatusId = task.StatusId,
                CategoryName = task.Category?.Name,
                IsCompleted = task.Status?.IsCompleted ?? false
            };

        public static CategoryResponse ToResponse(this Category category) =>
            new()
            {
                Id = category.Id,
                Name = category.Name
            };

        public static ExpenseResponse ToResponse(this Expense expense) =>
            new()
            {
                Id = expense.Id,
                Amount = expense.Amount,
                Currency = expense.Currency,
                SpentAt = expense.SpentAt,
                TaskId = expense.TaskId
            };

        public static StatusResponse ToResponse(this Status status) =>
            new()
            {
                Id = status.Id,
                IsCompleted = status.IsCompleted
            };
    }
}
