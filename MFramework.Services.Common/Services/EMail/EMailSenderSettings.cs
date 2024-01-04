namespace MFramework.Services.Common
{
    public partial class EMailSenderSettings
    {
        public int MailPort { get; set; }
        public string MailHost { get; set; }
        public string MailUsername { get; set; }
        public string MailPassword { get; set; }
        public bool MailEnableSsl { get; set; }
        public bool MailIsBodyHtml { get; set; }
        public string MailDisplayName { get; set; }
        public bool MailUseDefaultCredentials { get; set; }
    }
}
