using FullstackOpdracht.Util.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FullstackOpdracht.Util
{
    public class EmailSend : IEmailSend
    {
        private readonly EmailSettings _emailSettings;
        // Package Microsoft.Extensions.Option zal je moeten installeren
        //Microsoft.Extensions.Options provides a strongly typed way of specifying and accessing settings using dependency injection and acts 
        //as a bridge between configuration
        public EmailSend(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task SendEmailAsync(
        string email, string subject, string message)
        {
            var mail = new MailMessage(); // aanmaken van een mail-object
            mail.To.Add(new MailAddress(email));
            mail.From = new
            MailAddress("n.vansteenlandt@gmail.com"); // hier komt jullie Gmail-adres
            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = true;
            try
            {
                await SmtpMailAsync(mail);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public async Task SendEmailAttachmentAsync(string email, string subject, string message, List<MemoryStream> attachmentStream, string attachmentName)
        {
            var mail = new MailMessage(); // aanmaken van een mail-object
            mail.To.Add(new MailAddress(email));
            mail.From = new
            MailAddress("n.vansteenlandt@gmail.com"); // hier komt jullie Gmail-adres
            mail.Subject = subject;
            mail.Body = message;

            int attachmentIndex = 0;
            foreach (var item in attachmentStream)
            {
                mail.Attachments.Add(new Attachment(item, attachmentName));
                attachmentIndex++;
            }
            
            mail.IsBodyHtml = true;
            try
            {
                await SmtpMailAsync(mail);
            }
            catch (Exception ex)
            { throw ex; }
        }

        private async Task SmtpMailAsync(MailMessage mail)
        {
            using (var smtp = new SmtpClient(_emailSettings.MailServer))
            {
                smtp.Port = _emailSettings.MailPort;
                smtp.EnableSsl = true;
                smtp.Credentials =
                new NetworkCredential(_emailSettings.Sender,
                _emailSettings.Password);
                await smtp.SendMailAsync(mail);
            }
        }
    }
}
