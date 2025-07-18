using Microsoft.Extensions.Configuration;
using MimeKit;
using So2Baladna.Core.Dto;
using So2Baladna.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Infrastructure.Repositories.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> SendEmailAsync(EmailDto emailDto)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("So2Baladna", configuration["EmailSettings:From"]));
            message.To.Add(new MailboxAddress(emailDto.To, emailDto.To));
            message.Subject = emailDto.Subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailDto.Content
            };

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    await smtp.ConnectAsync(
                        configuration["EmailSettings:Smtp"],
                        int.Parse(configuration["EmailSettings:Port"]),
                        true // true = use SSL
                    );

                    await smtp.AuthenticateAsync(
                        configuration["EmailSettings:Username"], // ✅ fixed typo
                        configuration["EmailSettings:Password"]
                    );

                    await smtp.SendAsync(message);
                    return "Email sent successfully.";
                }
                catch (Exception ex)
                {
                    throw new Exception("Email sending failed: " + ex.Message);
                }
                finally
                {
                    await smtp.DisconnectAsync(true);
                }
            }
        }


    }
}
