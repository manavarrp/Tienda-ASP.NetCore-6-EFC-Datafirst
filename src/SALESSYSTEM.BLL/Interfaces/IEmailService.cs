namespace SALESSYSTEM.BLL.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string Email, string Subject, string MessageEmail);

    }
}
