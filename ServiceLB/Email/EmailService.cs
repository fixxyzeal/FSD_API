using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLB
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;

        private readonly int _smtpPort;

        private readonly string _fromAddress;

        private readonly string _fromAddressTitle;

        private readonly string _username;

        private readonly string _password;

        private readonly bool _enableSsl;

        private readonly bool _useDefaultCredentials;

        public EmailService(IConfiguration configuration)
        {
            _smtpServer = configuration["Email:SmtpServer"];

            _smtpPort = int.Parse(configuration["Email:SmtpPort"]);

            _smtpPort = _smtpPort == 0 ? 25 : _smtpPort;

            _fromAddress = Environment.GetEnvironmentVariable("SmtpUsername");

            _fromAddressTitle = configuration["FromAddressTitle"];

            _username = Environment.GetEnvironmentVariable("SmtpUsername");

            _password = Environment.GetEnvironmentVariable("SmtpPassword");

            _enableSsl = bool.Parse(configuration["Email:EnableSsl"]);

            _useDefaultCredentials = bool.Parse(configuration["Email:UseDefaultCredentials"]);
        }

        public async Task Send(string toAddress, string subject, string body, bool sendAsync = true)

        {
            MimeMessage mimeMessage = new MimeMessage(); // MIME : Multipurpose Internet Mail Extension
            mimeMessage.From.Add(new MailboxAddress(_fromAddressTitle, _fromAddress));

            mimeMessage.To.Add(new MailboxAddress(string.Empty, toAddress));

            mimeMessage.Subject = subject;

            BodyBuilder bodyBuilder = new MimeKit.BodyBuilder

            {
                HtmlBody = body
            };

            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync(_smtpServer, _smtpPort, _enableSsl).ConfigureAwait(false);

                await client.AuthenticateAsync(_username.Trim(), _password.Trim()).ConfigureAwait(false);
                if (sendAsync)

                {
                    await client.SendAsync(mimeMessage).ConfigureAwait(false);
                }
                else

                {
                    client.Send(mimeMessage);
                }

                client.Disconnect(true);
            }
        }
    }
}