using System.Net;
using System.Net.Mail;
using VaccinationSystemApi.Configuration;
using Microsoft.Extensions.Options;

namespace VaccinationSystemApi.Services
{
    public class MailService
    {
        readonly string smtpClientAddress = "smtp.poczta.onet.pl";
        readonly string _sender;
        readonly string _password;

        readonly SmtpClient _smtpClient;


        public MailService(IOptions<MailConfig> mailConfig)
        {
            var mailInfo = mailConfig.Value;
            _sender = mailInfo.Sender;
            _password = mailInfo.Password;

            _smtpClient = new SmtpClient(smtpClientAddress)
            {
                Port = 587,
                Credentials = new NetworkCredential(_sender, _password),
                EnableSsl = true,

            };
        }
        public void SendConfirmVaccinationMail(string receiver)
        {
            Send(receiver, "Vaccination confirmed", "Your vaccination appointment was confirmed");
        }

        public void SendCancelVaccinationMail(string receiver)
        {
            Send(receiver, "Vaccination canceled", "Your vaccination appointment was cancelled");
        }

        public void Send(string receiver, string subject, string body)
        {
            _smtpClient.Send(_sender, receiver, subject, body);
        }
    }
}
