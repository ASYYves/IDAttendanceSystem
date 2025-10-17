using MailKit.Net.Smtp;
using MimeKit;

namespace IDSystemBusinessLogic;
class Email
{
    public void SendEmail(string student, string id)
    {
        var message = new MimeMessage();

        message.From.Add(new MailboxAddress("PUPBC", "samplepupemail.com"));
        message.To.Add(new MailboxAddress(student, id));
        message.Subject = "Attendance";

 
        string emailText = $"You are now in\n{student}";

        message.Body = new TextPart("plain")
        {
            Text = emailText
        };

        using (var client = new SmtpClient())
        {
            var smtpHost = "sandbox.smtp.mailtrap.io";
            var smtpPort = 2525;
            var tsl = MailKit.Security.SecureSocketOptions.StartTls;

            client.Connect(smtpHost, smtpPort, tsl);

            var username = "9e559ece15bd0b";
            var password = "faf702ceac54db";

            client.Authenticate(username, password);

            client.Send(message);
            client.Disconnect(true);

        }
    }
}