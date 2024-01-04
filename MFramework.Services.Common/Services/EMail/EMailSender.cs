using Microsoft.Extensions.Options;
using System.ComponentModel.Design;
using System.Net;
using System.Net.Mail;

namespace MFramework.Services.Common
{

    public partial class EMailSender : IEMailSender
    {
        private readonly EMailSenderSettings MailSettings;

        public EMailSender(IOptions<EMailSenderSettings> options)
        {
            MailSettings = options?.Value;
        }

        public virtual void SendMail(string[] tos, string body, string subject, Attachment[] attachments = null)
        {
            SmtpClient client = new SmtpClient(MailSettings.MailHost)
            {
                UseDefaultCredentials = MailSettings.MailUseDefaultCredentials,
                Port = MailSettings.MailPort,
                EnableSsl = MailSettings.MailEnableSsl,
                Credentials = new NetworkCredential(MailSettings.MailUsername, MailSettings.MailPassword)
            };

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(MailSettings.MailUsername, MailSettings.MailDisplayName);

            foreach (var to in tos)
            {
                mailMessage.To.Add(to);
            }

            mailMessage.Body = body;
            mailMessage.IsBodyHtml = MailSettings.MailIsBodyHtml;
            mailMessage.Subject = subject;

            //if (attachments != null) for (int i = 0; i < attachments.Length; i++) mailMessage.Attachments.Add(attachments[i]);
            if (attachments != null) foreach (var attachment in attachments) mailMessage.Attachments.Add(attachment);

            client.Send(mailMessage);
        }
    }
}
