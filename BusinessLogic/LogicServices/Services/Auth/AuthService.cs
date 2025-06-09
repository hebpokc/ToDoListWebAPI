using BusinessLogic.LogicServices.Interfaces;
using BusinessLogic.LogicServices.Interfaces.Auth;
using DataAccess.DataRepositories.Interfaces;
using DataAccess.DataRepositories.Repositories;
using DataAccess.Models;
using DataAccess.Requests;

namespace BusinessLogic.LogicServices.Services
{
    /// <summary>
    /// Предоставляет бизнес-логику для операций, связанных с пользователем: регистрация и вход.
    /// </summary>
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="AuthService"/>.
        /// </summary>
        /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
        /// <param name="passwordHasher">Сервис для хеширования паролей.</param>
        /// <param name="jwtProvider">Поставщик JWT-токенов.</param>
        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        /// <summary>
        /// Выполняет аутентификацию пользователя и возвращает JWT-токен при успешном входе.
        /// </summary>
        /// <param name="email">Email пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>JWT-токен в случае успешной аутентификации.</returns>
        /// <exception cref="UnauthorizedAccessException">Выбрасывается, если email или пароль неверны.</exception>
        public async Task<string> Login(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user != null)
            {
                var result = _passwordHasher.Verify(password, user.PasswordHash);

                if (!result)
                {
                    throw new UnauthorizedAccessException("Invalid email or password.");
                }

                var token = _jwtProvider.GenerateToken(user);

                return token;
            }

            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        /// <summary>
        /// Регистрирует нового пользователя с указанным именем, email и паролем.
        /// </summary>
        /// <param name="username">Имя пользователя.</param>
        /// <param name="email">Адрес электронной почты.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <exception cref="InvalidOperationException">Выбрасывается, если пользователь с таким email уже существует.</exception>
        public async Task Register(string username, string email, string password)
        {
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser != null)
                throw new InvalidOperationException("A user with this email already exists.");

            var hashedPassword = _passwordHasher.Generate(password);

            var user = new ApplicationUser
            {
                Username = username,
                Email = email,
                PasswordHash = hashedPassword
            };

            await _userRepository.CreateAsync(user);
        }
    }
}
