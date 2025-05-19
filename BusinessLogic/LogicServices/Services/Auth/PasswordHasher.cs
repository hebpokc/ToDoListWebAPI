using BusinessLogic.LogicServices.Interfaces.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.LogicServices.Services.Auth
{
    /// <summary>
    /// Реализация интерфейса <see cref="IPasswordHasher"/>, использующая библиотеку BCrypt для безопасного хеширования паролей.
    /// </summary>
    public class PasswordHasher : IPasswordHasher
    {
        /// <summary>
        /// Генерирует защищённый хэш для указанного пароля, используя алгоритм BCrypt.
        /// </summary>
        /// <param name="password">Пароль в открытом виде, который необходимо захешировать.</param>
        /// <returns>Хэшированный пароль в виде строки.</returns>
        public string Generate(string password) => 
            BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        /// <summary>
        /// Проверяет, соответствует ли указанный открытый пароль сохранённому хэшированному значению.
        /// </summary>
        /// <param name="password">Пароль в открытом виде, который необходимо проверить.</param>
        /// <param name="hashedPassword">Сохранённый хэшированный пароль.</param>
        /// <returns><see langword="true"/>, если пароли совпадают; иначе — <see langword="false"/>.</returns>
        public bool Verify(string password, string hashedPassword) =>
            BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }
}
