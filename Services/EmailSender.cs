using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using System.Text.Encodings.Web;

namespace Integrity.Services;

public class EmailSender
{
    public EmailSender(){}

    public void SendEmailAsync(IConfiguration configuration, string receiverEmail, string subject, string text, string callbackUrl)
    {
        var apiInstance = new TransactionalEmailsApi();

        string? SenderName = configuration?["BrevoAPI:SenderName"];
        string? SenderEmail = configuration?["BrevoAPI:SenderEmail"];
        SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);

        SendSmtpEmailTo smtpEmailTo = new(receiverEmail);
        List<SendSmtpEmailTo> To = new()
        {
            smtpEmailTo
        };

        string HtmlContent = $"{text} by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
        string TextContent = null;

        try
        {
            var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, HtmlContent, TextContent, subject);
            CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
            Console.WriteLine("Brevo Response: " + result.ToJson());
        }
        catch (Exception e)
        {
            Console.WriteLine("We have an exception " + e.Message);
        }
    }

    public void SendEmailToAdmin(string name, string text)
    {
        var apiInstance = new TransactionalEmailsApi();

        string? SenderName = "F3T1W";
        string? SenderEmail = "particular0010abyss@gmail.com";
        SendSmtpEmailSender Email = new(SenderName, SenderEmail);

        string? ToEmail = "particular0010abyss@gmail.com";
        SendSmtpEmailTo smtpEmailTo = new(ToEmail);
        List<SendSmtpEmailTo> To = new()
        {
            smtpEmailTo
        };

        try
        {
            var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, null, text, name);
            CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
            Console.WriteLine("Brevo Response: " + result.ToJson());
        }
        catch (Exception e)
        {
            Console.WriteLine("We have an exception " + e.Message);
        }
    }
}