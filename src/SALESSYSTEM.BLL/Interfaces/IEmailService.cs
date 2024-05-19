namespace SALESSYSTEM.BLL.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string EmailDestination, string Subject, string MessageEmail);

    }
}
