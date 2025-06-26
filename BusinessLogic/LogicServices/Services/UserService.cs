using BusinessLogic.LogicServices.Interfaces;
using BusinessLogic.LogicServices.Interfaces.Auth;
using DataAccess.DataRepositories.Interfaces;
using DataAccess.DataRepositories.Repositories;
using DataAccess.Models;
using DataAccess.Requests;

namespace BusinessLogic.LogicServices.Services
{
    /// <summary>
    /// Реализация сервиса для работы с пользователями.
    /// Содержит бизнес-логику для обновления и удаления пользователей.
    /// </summary>
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="UserService"/>.
        /// </summary>
        /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
        /// <param name="passwordHasher">Сервис для хэширования паролей.</param>
        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Получает пользователя по его уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор пользователя.</param>
        /// <returns>Объект пользователя или null, если не найден.</returns>
        public async Task<ApplicationUser?> GetByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Получает пользователя по адресу электронной почты.
        /// </summary>
        /// <param name="email">Email пользователя.</param>
        /// <returns>Объект пользователя или null, если не найден.</returns>
        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        /// <summary>
        /// Обновляет данные пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="request">Новые данные пользователя.</param>
        /// <exception cref="InvalidOperationException">Выбрасывается, если пользователь не существует.</exception>
        public async Task UpdateAsync(Guid id, UserUpdateRequest request)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);

            if (existingUser == null)
                throw new InvalidOperationException("Пользователь не найден.");

            existingUser.Username = request.Username;
            existingUser.Email = request.Email;

            if (!string.IsNullOrWhiteSpace(request.CurrentPassword))
            {
                if (!_passwordHasher.Verify(request.CurrentPassword, existingUser.PasswordHash))
                    throw new ArgumentException("Текущий пароль указан неверно");

                if (!string.IsNullOrWhiteSpace(request.NewPassword))
                {
                    existingUser.PasswordHash = _passwordHasher.Generate(request.NewPassword);
                }
            }

            await _userRepository.UpdateAsync(existingUser);
        }

        /// <summary>
        /// Удаляет пользователя по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
