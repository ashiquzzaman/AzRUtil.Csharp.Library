using AzRUtil.Csharp.Library.Constants;

namespace AzRUtil.Csharp.Library.Models
{
    public class EmailOptions
    {
        public LibConstants.EmailClient EmailClient { get; set; }
        public string AppKey { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool IsSsl { get; set; }
        public string FromEmail { get; set; }
        public string SenderName { get; set; }
    }
}