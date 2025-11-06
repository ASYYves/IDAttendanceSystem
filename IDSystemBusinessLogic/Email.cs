using MailKit.Net.Smtp;
using MimeKit;
using System;

namespace IDSystemBusinessLogic
{
    
    public static class Email
    {
        
        private static Smtp _settings;

        
        public static void Initialize(Smtp settings)
        {
            _settings = settings;
        }

        
        public static void SendEmail(string student, string id)
        {
            
            if (_settings == null)
            {
                
                throw new InvalidOperationException("Email settings have not been initialized.");

            }

            var message = new MimeMessage();

            
            message.From.Add(new MailboxAddress(_settings.FromName, _settings.FromAddress));
            message.To.Add(new MailboxAddress(student, id));
            message.Subject = "Attendance";

            string emailText = $"You are now in\n{student}";
            message.Body = new TextPart("plain")
            {
                Text = emailText
            };

            using (var client = new SmtpClient())
            {
                                var smtpHost = _settings.Host;
                var smtpPort = _settings.Port;
                var tsl = MailKit.Security.SecureSocketOptions.StartTls;

                client.Connect(smtpHost, smtpPort, tsl);

                var username = _settings.Username;
                var password = _settings.Password;
                client.Authenticate(username, password);

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}