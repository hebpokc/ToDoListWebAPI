using DataAccess.DataRepositories.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.DataRepositories.Repositories
{
    /// <summary>
    /// Реализация репозитория для работы с пользователями.
    /// Использует асинхронные методы для создания, чтения, обновления и удаления пользователей.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="UserRepository"/>.
        /// </summary>
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Асинхронно создаёт нового пользователя в базе данных.
        /// </summary>
        /// <param name="user">Пользователь, которого нужно добавить.</param>
        /// <returns>Созданного пользователя.</returns>
        public async Task<ApplicationUser> CreateAsync(ApplicationUser user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// Асинхронно удаляет пользователя по указанному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Асинхронно получает пользователя по его email-адресу.
        /// Также загружает связанные задачи пользователя.
        /// </summary>
        /// <param name="email">Email пользователя.</param>
        /// <returns>Пользователя или null, если пользователь не найден.</returns>
        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// Асинхронно получает пользователя по его уникальному идентификатору.
        /// Также загружает связанные задачи пользователя.
        /// </summary>
        /// <param name="id">Уникальный идентификатор пользователя.</param>
        /// <returns>Пользователя или null, если пользователь не найден.</returns>
        public async Task<ApplicationUser?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// Асинхронно обновляет данные существующего пользователя.
        /// </summary>
        /// <param name="user">Объект пользователя с обновлёнными данными.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        public async Task UpdateAsync(ApplicationUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Асинхронно обновляет пароль пользователя.
        /// </summary>
        /// <param name="user">Объект пользователя с обновлёнными данными.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>

        public async Task ChangePasswordAsync(ApplicationUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
