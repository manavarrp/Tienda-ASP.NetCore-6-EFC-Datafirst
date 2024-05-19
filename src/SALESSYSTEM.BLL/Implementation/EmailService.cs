using SALESSYSTEM.BLL.Interfaces;
using SALESSYSTEM.DAL.Interfaces;
using SALESSYSTEM.Domain.Entities;
using System.Net;
using System.Net.Mail;

namespace SALESSYSTEM.BLL.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly IGenericRepository<Configuration> _repository;

        public  EmailService(IGenericRepository<Configuration> repository)
        {
            _repository = repository;
        }

        public async Task<bool> SendEmail(string EmailDestination, string Subject, string MessageEmail)
        {
            try
            {
                IQueryable<Configuration> query = await _repository.GetEntityQuery(c => c.Resource.Equals("Servicio_Correo"));

                Dictionary<string, string> Config = query.ToDictionary(keySelector: c => c.Property, elementSelector: c => c.Value);

                var credentials = new NetworkCredential(Config["correo"], Config["clase"]);

                var email = new MailMessage()
                {
                    From =new MailAddress(Config["correo"], Config["alias"]),
                    Subject = Subject,
                    Body = MessageEmail,
                    IsBodyHtml = true
                };

                email.To.Add(new MailAddress(EmailDestination));

                var clientServer = new SmtpClient()
                {
                    Host = Config["host"],
                    Port = int.Parse(Config["port"]),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    EnableSsl = true
                };

                clientServer.Send(email);

                return true;
            }
            catch  
            {
                return false;
            }
        }
    }
}
