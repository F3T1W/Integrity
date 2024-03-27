using System.Net.Mail;
using System.Net;
using Integrity.Services;

namespace Integrity.Pages;

public partial class Contacts
{
    private readonly EmailSender _emailSender;

    private string Name { get; set; } = "";
    private string Email { get; set; } = "";
    private string Message { get; set; } = "";

    public Contacts()
    {
        _emailSender = new EmailSender();
    }

    public void SendEmailAsync()
    {
        _emailSender.SendEmailToAdmin(Name, Message);

        ResetValues();
    }

    private void ResetValues()
    {
        Name = "";
        Email = "";
        Message = "";
    }
}
