using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;

namespace ShippingCo.Services
{
    public class MailService : IMailService
    {
        public async Task SendMail(string to, string firstName, string lastName, string from, string body)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(firstName + " " + lastName, from));
            emailMessage.To.Add(new MailboxAddress("", to));
            emailMessage.Subject = "Shipping CO";
            emailMessage.Body = new TextPart("plain") { Text = body };

            using (var client = new SmtpClient())
            {
                client.LocalDomain = "smtp.gmail.com";
                await client.ConnectAsync("smtp.gmail.com", 465, SecureSocketOptions.Auto).ConfigureAwait(false);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);

            }


        }
    }
}
