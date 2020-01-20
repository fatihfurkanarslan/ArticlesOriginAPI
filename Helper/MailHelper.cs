using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Helper
{
    public class MailHelper
    {
        private readonly IOptions<MailSettings> mailConfig;
        public MailHelper(IOptions<MailSettings> _mailConfig)
        {
            mailConfig = _mailConfig;
        }
        public bool SendMail(string body, string to, string subject, bool isHtml = true)
        {
            return SendMail(body, new List<string> { to }, subject, isHtml);
        }

        public bool SendMail(string body, List<string> to, string subject, bool isHtml = true)
        {
            bool result = false;

            try
            {
                var message = new MailMessage();
                message.From = new MailAddress(mailConfig.Value.MailUser);

                to.ForEach(x =>
                {
                    message.To.Add(new MailAddress(x));
                });

                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;

                using (var smtp = new SmtpClient(
                    mailConfig.Value.MailHost,
                    mailConfig.Value.MailPort))
                {
                    smtp.EnableSsl = true;
                    smtp.Credentials =
                        new NetworkCredential(
                            mailConfig.Value.MailUser,
                            mailConfig.Value.MailPass);

                    smtp.Send(message);
                    result = true;
                }
            }
            catch (Exception)
            {

            }

            return result;
        }
    }
}
