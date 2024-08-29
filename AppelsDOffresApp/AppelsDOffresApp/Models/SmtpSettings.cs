namespace AppelsDOffresApp.Models
{
    public class SmtpSettings
    {
        public string Server { get; set; }
        public int Port { get; set; } = 587; // Default port for TLS
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Username { get; set; }
        public string AppPassword { get; set; }
    }

}
