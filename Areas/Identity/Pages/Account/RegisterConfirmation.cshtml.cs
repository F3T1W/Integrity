// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.Text;
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;
using Integrity.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;

namespace Integrity.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<IntegrityUser> _userManager;
        private readonly IEmailSender _sender;

        private string Message { get; set; } = "";

        public RegisterConfirmationModel(UserManager<IntegrityUser> userManager, IEmailSender sender)
        {
            _userManager = userManager;
            _sender = sender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public bool DisplayConfirmAccountLink { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string EmailConfirmationUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }
            returnUrl = returnUrl ?? Url.Content("~/");

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            Email = email;
            // Once you add a real email sender, you should remove this code that lets you confirm the account
            DisplayConfirmAccountLink = true;

            return Page();
        }

        private async Task SendMail(string email)
        {
            try
            {
                using MailMessage message = new();
                message.From = new MailAddress("particular0010abyss@gmail.com");
                message.To.Add(new MailAddress(email));
                message.Subject = "Welcome to Integrity";
                message.IsBodyHtml = true;
                message.Body = $"Thank you for your energy: <a id=\"confirm-link\" href=\"{EmailConfirmationUrl}\">Click here to confirm your account</a>";

                using SmtpClient smtp = new("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("particular0010abyss@gmail.com", "lgcc rsbc rbup yinm");
                await smtp.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw;
            }
        }
    }
}
