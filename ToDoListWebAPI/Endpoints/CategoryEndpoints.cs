using BusinessLogic.LogicServices.Services;
using DataAccess.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ToDoListWebAPI.Endpoints
{
    /// <summary>
    /// Содержит минимальные API-эндпоинты для работы с расходами.
    /// Предоставляет маршруты для создания, получения, обновления и удаления категорий.
    /// </summary>
    public static class CategoryEndpoints
    {
        /// <summary>
        /// Регистрирует эндпоинты для работы с категориями.
        /// </summary>
        /// <param name="app">Построитель маршрутов.</param>
        /// <returns>Объект <see cref="IEndpointRouteBuilder"/> после добавления маршрутов.</returns>
        public static IEndpointRouteBuilder MapCategoriesEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/category");

            group.MapPost("/create", Create).RequireAuthorization();
            group.MapGet("/getById/{id:guid}", GetById).RequireAuthorization();
            group.MapPut("/update/{id:guid}", Update).RequireAuthorization();
            group.MapDelete("/delete/{id:guid}", Delete).RequireAuthorization();

            return app;
        }

        /// <summary>
        /// Обрабатывает запрос на создание новой категории.
        /// </summary>
        /// <param name="request">Данные для создания категории.</param>
        /// <param name="categoryService">Сервис для работы с категориями.</param>
        /// <returns>Результат операции: 200 OK при успешном создании.</returns>
        [HttpPost]
        private static async Task<IResult> Create(
            CategoryCreateRequest request,
            CategoryService categoryService)
        {
            await categoryService.CreateAsync(request.Name);

            return Results.Ok();
        }

        /// <summary>
        /// Обрабатывает запрос на получение категории по ID.
        /// </summary>
        /// <param name="id">Уникальный идентификатор категории.</param>
        /// <param name="categoryService">Сервис для работы с категориями.</param>
        /// <returns>Результат операции: 200 OK с данными категории или 404, если не найдена.</returns>
        private static async Task<IResult> GetById(
            Guid id,
            CategoryService categoryService)
        {
            var category = await categoryService.GetByIdAsync(id);

            if (category == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(category);
        }

        /// <summary>
        /// Обрабатывает запрос на обновление существующей категории.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="request">Новые данные для обновления категории.</param>
        /// <param name="categoryService">Сервис для работы с категориями.</param>
        /// <returns>Результат операции: 200 OK при успехе.</returns>
        private static async Task<IResult> Update(
            Guid id,
            CategoryUpdateRequest request,
            CategoryService categoryService)
        {
            await categoryService.UpdateAsync(id, request.Name);

            return Results.Ok();
        }

        /// <summary>
        /// Обрабатывает запрос на удаление категории по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="categoryService">Сервис для работы с категориями.</param>
        /// <returns>Результат операции: 200 OK при успехе.</returns>
        private static async Task<IResult> Delete(
            Guid id,
            CategoryService categoryService)
        {
            await categoryService.DeleteAsync(id);

            return Results.Ok();
        }
    }
}
