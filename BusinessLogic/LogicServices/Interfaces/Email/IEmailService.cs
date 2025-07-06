namespace BusinessLogic.LogicServices.Interfaces.Email
{
    /// <summary>
    /// Интерфейс для отправки email сообщений.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Отправить email.
        /// </summary>
        /// <param name="to">Адресат</param>
        /// <param name="subject">Тема письма</param>
        /// <param name="htmlContent">HTML-содержимое письма</param>
        /// <returns>Асинхронная задача</returns>
        Task SendEmailAsync(string to, string subject, string htmlContent);
    }

}
