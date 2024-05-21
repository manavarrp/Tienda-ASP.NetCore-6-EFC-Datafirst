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

                var credentials = new NetworkCredential(Config["correo"], Config["clave"]);

                var email = new MailMessage()
                {
                    From = new MailAddress(Config["correo"], Config["alias"]),
                    Subject = Subject,
                    Body = MessageEmail,
                    IsBodyHtml = true
                };

                email.To.Add(new MailAddress(EmailDestination));

                // Configurar el cliente SMTP
                SmtpClient clientServer = new SmtpClient()
                {
                    Host = Config["host"],
                    Port = int.Parse(Config["port"]),
                    Credentials = credentials,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    EnableSsl = true
                };

                // Ignorar errores de certificado SSL (solo para desarrollo)
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                clientServer.Send(email);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex}");
                return false;
            }
        }

    }
}
