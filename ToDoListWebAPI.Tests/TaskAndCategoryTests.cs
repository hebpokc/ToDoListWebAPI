using BusinessLogic.LogicServices.Services;
using DataAccess.DataRepositories.Interfaces;
using DataAccess.Models;
using Moq;
using Xunit;

namespace ToDoListWebAPI.Tests
{
    /// <summary>
    /// Unit-тесты TaskService с использованием Moq.
    /// </summary>
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _repositoryMock;
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            _repositoryMock = new Mock<ITaskRepository>();
            _taskService = new TaskService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_TaskExists_ReturnsTask()
        {
            var task = new TaskEntity { Id = Guid.NewGuid(), Title = "Тестовая задача" };

            _repositoryMock
                .Setup(r => r.GetByIdAsync(task.Id))
                .ReturnsAsync(task);

            var result = await _taskService.GetByIdAsync(task.Id);

            Assert.NotNull(result);
            Assert.Equal("Тестовая задача", result!.Title);
        }

        [Fact]
        public async Task GetByIdAsync_TaskNotFound_ReturnsNull()
        {
            _repositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((TaskEntity?)null);

            var result = await _taskService.GetByIdAsync(Guid.NewGuid());

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllByUserIdAsync_ReturnsOnlyUserTasks()
        {
            var userId = Guid.NewGuid();
            var tasks = new List<TaskEntity>
            {
                new() { Id = Guid.NewGuid(), UserId = userId, Title = "Задача 1" },
                new() { Id = Guid.NewGuid(), UserId = userId, Title = "Задача 2" }
            };

            _repositoryMock
                .Setup(r => r.GetAllByUserIdAsync(userId))
                .ReturnsAsync(tasks);

            var result = await _taskService.GetAllByUserIdAsync(userId);

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task CreateAsync_CreatesTaskWithCorrectFields()
        {
            var categoryId = Guid.NewGuid();
            var statusId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var dueDate = new DateTime(2026, 12, 31);

            TaskEntity? created = null;
            _repositoryMock
                .Setup(r => r.CreateAsync(It.IsAny<TaskEntity>()))
                .Callback<TaskEntity>(t => created = t)
                .Returns(Task.CompletedTask);

            var result = await _taskService.CreateAsync(
                "Заголовок", "Описание", dueDate, categoryId, statusId, userId);

            Assert.NotNull(created);
            Assert.Equal("Заголовок", created!.Title);
            Assert.Equal("Описание", created.Description);
            Assert.Equal(dueDate, created.DueDate);
            Assert.Equal(categoryId, created.CategoryId);
            Assert.Equal(statusId, created.StatusId);
            Assert.Equal(userId, created.UserId);
            Assert.Equal(result.Id, created.Id);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesTaskWithGivenId()
        {
            var id = Guid.NewGuid();

            TaskEntity? updated = null;
            _repositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<TaskEntity>()))
                .Callback<TaskEntity>(t => updated = t)
                .Returns(Task.CompletedTask);

            await _taskService.UpdateAsync(
                id, "Новый заголовок", "Новое описание",
                new DateTime(2026, 6, 1), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            Assert.NotNull(updated);
            Assert.Equal(id, updated!.Id);
            Assert.Equal("Новый заголовок", updated.Title);
        }

        [Fact]
        public async Task DeleteAsync_CallsRepository()
        {
            var id = Guid.NewGuid();

            await _taskService.DeleteAsync(id);

            _repositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task MarkAsCompletedAsync_TaskExists_SetsNewStatus()
        {
            var task = new TaskEntity
            {
                Id = Guid.NewGuid(),
                StatusId = Guid.NewGuid()
            };
            var completedStatusId = Guid.NewGuid();

            _repositoryMock
                .Setup(r => r.GetByIdAsync(task.Id))
                .ReturnsAsync(task);

            await _taskService.MarkAsCompletedAsync(task.Id, completedStatusId);

            Assert.Equal(completedStatusId, task.StatusId);

            _repositoryMock.Verify(r => r.UpdateAsync(task), Times.Once);
        }

        [Fact]
        public async Task MarkAsCompletedAsync_TaskNotFound_ThrowsInvalidOperation()
        {
            _repositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((TaskEntity?)null);

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => _taskService.MarkAsCompletedAsync(Guid.NewGuid(), Guid.NewGuid()));

            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<TaskEntity>()), Times.Never);
        }
    }

    /// <summary>
    /// Unit-тесты CategoryService с использованием Moq.
    /// </summary>
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _repositoryMock;
        private readonly CategoryService _categoryService;

        public CategoryServiceTests()
        {
            _repositoryMock = new Mock<ICategoryRepository>();
            _categoryService = new CategoryService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_CategoryExists_ReturnsCategory()
        {
            var category = new Category { Id = Guid.NewGuid(), Name = "Работа" };

            _repositoryMock
                .Setup(r => r.GetByIdAsync(category.Id))
                .ReturnsAsync(category);

            var result = await _categoryService.GetByIdAsync(category.Id);

            Assert.NotNull(result);
            Assert.Equal("Работа", result!.Name);
        }

        [Fact]
        public async Task GetByIdAsync_CategoryNotFound_ReturnsNull()
        {
            _repositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Category?)null);

            var result = await _categoryService.GetByIdAsync(Guid.NewGuid());

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllCategories()
        {
            var categories = new List<Category>
            {
                new() { Id = Guid.NewGuid(), Name = "Работа" },
                new() { Id = Guid.NewGuid(), Name = "Личное" }
            };

            _repositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(categories);

            var result = await _categoryService.GetAllAsync();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task CreateAsync_CreatesCategoryWithName()
        {
            Category? created = null;
            _repositoryMock
                .Setup(r => r.CreateAsync(It.IsAny<Category>()))
                .Callback<Category>(c => created = c)
                .Returns(Task.CompletedTask);

            await _categoryService.CreateAsync("Новая категория");

            Assert.NotNull(created);
            Assert.Equal("Новая категория", created!.Name);

            _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Category>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesCategoryWithGivenId()
        {
            var id = Guid.NewGuid();

            Category? updated = null;
            _repositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<Category>()))
                .Callback<Category>(c => updated = c)
                .Returns(Task.CompletedTask);

            await _categoryService.UpdateAsync(id, "Обновлённая категория");

            Assert.NotNull(updated);
            Assert.Equal(id, updated!.Id);
            Assert.Equal("Обновлённая категория", updated.Name);
        }

        [Fact]
        public async Task DeleteAsync_CallsRepository()
        {
            var id = Guid.NewGuid();

            await _categoryService.DeleteAsync(id);

            _repositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }
    }
}
