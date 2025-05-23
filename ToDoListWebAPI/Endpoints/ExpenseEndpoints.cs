using BusinessLogic.LogicServices.Services;
using DataAccess.Requests;

namespace ToDoListWebAPI.Endpoints
{
    /// <summary>
    /// Содержит минимальные API-эндпоинты для работы с расходами.
    /// Предоставляет маршруты для создания, получения, обновления и удаления расходов.
    /// </summary>
    public static class ExpenseEndpoints
    {
        /// <summary>
        /// Регистрирует эндпоинты для работы с расходами.
        /// </summary>
        /// <param name="app">Построитель маршрутов.</param>
        /// <returns>Объект <see cref="IEndpointRouteBuilder"/> после добавления маршрутов.</returns>
        public static IEndpointRouteBuilder MapExpensesEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/expenses");

            group.MapPost("", Create).RequireAuthorization();
            group.MapGet("{id:guid}", GetById).RequireAuthorization();
            group.MapPut("{id:guid}", Update).RequireAuthorization();
            group.MapDelete("{id:guid}", Delete).RequireAuthorization();

            return app;
        }

        /// <summary>
        /// Обрабатывает запрос на создание нового расхода.
        /// </summary>
        /// <param name="request">Данные для создания расхода.</param>
        /// <param name="expenseService">Сервис для работы с расходами.</param>
        /// <returns>Результат операции: 200 OK при успешном создании.</returns>
        private static async Task<IResult> Create(
            ExpenseCreateRequest request,
            ExpenseService expenseService)
        {
            await expenseService.CreateAsync(
                request.Amount,
                request.Currency,
                request.SpentAt,
                request.TaskId);

            return Results.Ok();
        }

        /// <summary>
        /// Обрабатывает запрос на получение расхода по ID.
        /// </summary>
        /// <param name="id">Идентификатор расхода.</param>
        /// <param name="expenseService">Сервис для работы с расходами.</param>
        /// <returns>Результат операции: 200 OK с данными расхода или 404, если не найден.</returns>
        private static async Task<IResult> GetById(
            Guid id,
            ExpenseService expenseService)
        {
            var expense = await expenseService.GetByIdAsync(id);

            if (expense == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(expense);
        }

        /// <summary>
        /// Обрабатывает запрос на обновление существующего расхода.
        /// </summary>
        /// <param name="id">Идентификатор расхода.</param>
        /// <param name="request">Новые данные расхода.</param>
        /// <param name="expenseService">Сервис для работы с расходами.</param>
        /// <returns>Результат операции: 200 OK при успехе.</returns>
        private static async Task<IResult> Update(
            Guid id,
            ExpenseUpdateRequest request,
            ExpenseService expenseService)
        {
            await expenseService.UpdateAsync(
                id,
                request.Amount,
                request.Currency,
                request.SpentAt);

            return Results.Ok();
        }

        /// <summary>
        /// Обрабатывает запрос на удаление расхода по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор расхода.</param>
        /// <param name="expenseService">Сервис для работы с расходами.</param>
        /// <returns>Результат операции: 200 OK при успехе.</returns>
        private static async Task<IResult> Delete(
            Guid id,
            ExpenseService expenseService)
        {
            await expenseService.DeleteAsync(id);
            return Results.Ok();
        }
    }
}
