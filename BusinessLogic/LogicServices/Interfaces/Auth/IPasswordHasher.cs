using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.LogicServices.Interfaces.Auth
{
    /// <summary>
    /// Определяет методы для безопасного хеширования и проверки паролей.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Генерирует защищённый хэш для указанного пароля.
        /// </summary>
        /// <param name="password">Пароль в открытом виде.</param>
        /// <returns>Хэшированный пароль.</returns>
        public string Generate(string password);

        /// <summary>
        /// Проверяет, соответствует ли указанный открытый пароль сохранённому хэшированному значению.
        /// </summary>
        /// <param name="password">Пароль в открытом виде.</param>
        /// <param name="hashedPassword">Сохранённый хэшированный пароль.</param>
        /// <returns><see langword="true"/>, если пароли совпадают; иначе — <see langword="false"/>.</returns>
        bool Verify(string password, string hashedPassword);
    }
}
