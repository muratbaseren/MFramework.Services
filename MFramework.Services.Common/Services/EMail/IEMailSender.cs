using System.Net.Mail;

namespace MFramework.Services.Common
{
    public interface IEMailSender
    {
        void SendMail(string[] tos, string body, string subject, Attachment[] attachments = null);
    }
}
