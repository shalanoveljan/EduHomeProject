using System;
using System.Net;
using System.Net.Mail;
using EduHome.Service.ExternalServices.Interfaces;

namespace EduHome.Service.ExternalServices.Implementations
{
    public class EmailService : IEmailService
    {
        public async Task SendEmail(string to, string subject, string body)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("elcan.salanov2004@gmail.com", "jfflrccnwhblwoax");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("elcan.salanov2004@gmail.com","EduHome App");
            mailMessage.To.Add(to);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;
            client.Send(mailMessage);
        }
    }
}

