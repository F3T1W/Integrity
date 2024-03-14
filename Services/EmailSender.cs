using Integrity.Services.Interfaces;
using System.Net.Mail;
using System.Net;

namespace Integrity.Services;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string message)
    {
        var client = new SmtpClient("smtp.gmail.com", 465)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("particular0010abyss@gmail.com", "lgcc rsbc rbup yinm")
        };

        return client.SendMailAsync(
            new MailMessage(from: email,
                            to: "particular0010abyss@gmail.com",
                            subject,
                            message
                            ));
    }
}
