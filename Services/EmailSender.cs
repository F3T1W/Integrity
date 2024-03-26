using Newtonsoft.Json.Linq;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using System.Diagnostics;
using System.Text.Encodings.Web;

namespace Integrity.Services;

public class EmailSender
{
    public void SendEmailAsync(string receiverEmail, string subject, string text, string callbackUrl)
    {
        var apiInstance = new TransactionalEmailsApi();

        string SenderName = "F3T1W";
        string SenderEmail = "particular0010abyss@gmail.com";
        SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);

        string ToEmail = receiverEmail;
        SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(ToEmail);
        List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
        To.Add(smtpEmailTo);

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
}
