using System.Net.Mail;
using System.Net;

namespace Integrity.Pages;

public partial class Contacts
{
    private string Name { get; set; } = "";
    private string Email { get; set; } = "";
    private string Message { get; set; } = "";

    public async Task SendEmailAsync()
    {
        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("particular0010abyss@gmail.com", "lgcc rsbc rbup yinm")
        };

        var mailMessage = new MailMessage(from: "particular00100abyss@gmail.com",
                                            to: "particular0010abyss@gmail.com",
                                            Name,
                                            Message);

        await client.SendMailAsync(mailMessage);

        ResetValues();
    }

    private void ResetValues()
    {
        Name = "";
        Email = "";
        Message = "";
    }
}
