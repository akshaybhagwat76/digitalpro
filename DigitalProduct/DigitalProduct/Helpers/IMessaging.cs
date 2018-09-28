using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DigitalProduct.Helpers
{
    public interface IMessaging
    {
        Task SendEmail(string toEmail, string subject, string msg);
    }

    public class Messaging : IMessaging
    {
        public Task SendEmail(string toEmails, string subject, string msg)
        {
            try
            {
                var debugEmail = ConfigurationManager.AppSettings["DebugEmail"];
                toEmails = !string.IsNullOrWhiteSpace(debugEmail) ? debugEmail : toEmails;
                var mail = new MailMessage
                {
                    From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"]),
                    Subject = subject,
                    Body = msg,
                    IsBodyHtml = true
                };
                mail.To.Add(toEmails);
                var smtp = new SmtpClient();
                smtp.Send(mail);
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
