using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using System.Text.Encodings.Web;

namespace Integrity.Services;

public class EmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendEmail(string receiverEmail, string subject, string text, string callbackUrl)
    {
        var apiInstance = new TransactionalEmailsApi();

        string? SenderName = _configuration["BrevoAPI:SenderName"];
        string? SenderEmail = _configuration["BrevoAPI:SenderEmail"];
        SendSmtpEmailSender Email = new(SenderName, SenderEmail);

        SendSmtpEmailTo smtpEmailTo = new(receiverEmail);
        List<SendSmtpEmailTo> To = new()
        {
            smtpEmailTo
        };

        string HtmlContent = $"{text} by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
        string? TextContent = null;

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

    public void SendEmail(string name, string text)
    {
        var apiInstance = new TransactionalEmailsApi();

        string? SenderName = _configuration["BrevoAPI:SenderName"];
        string? SenderEmail = _configuration["BrevoAPI:SenderEmail"];
        SendSmtpEmailSender Email = new(SenderName, SenderEmail);

        string? ToEmail = _configuration["BrevoAPI:SenderEmail"];
        SendSmtpEmailTo smtpEmailTo = new(ToEmail);
        List<SendSmtpEmailTo> To = new()
        {
            smtpEmailTo
        };

        string? TextContent = text;

        try
        {
            var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, null, TextContent, name);
            CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
            Console.WriteLine("Brevo Response: " + result.ToJson());
        }
        catch (Exception e)
        {
            Console.WriteLine("We have an exception " + e.Message);
        }
    }
}