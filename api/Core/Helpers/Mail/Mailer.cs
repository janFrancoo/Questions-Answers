using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Core.Helpers.Mail
{
    public class Mailer : IMailer
    {
        private readonly SmtpSettings _smtpSettings;

        public Mailer(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        [Obsolete]
        public async Task SendMailAsync(string mail, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderMail));
                message.To.Add(new MailboxAddress(mail));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = body
                };

                using (var client = new SmtpClient())
                {
                    client.CheckCertificateRevocation = false;
                    await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, true);
                    await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            } 
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }


            await Task.CompletedTask;
        }
    }
}
