namespace BusinessLogic.LogicServices.Services.Email
{
    /// <summary>
    /// Модель конфигурации для почты
    /// </summary>
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

}
