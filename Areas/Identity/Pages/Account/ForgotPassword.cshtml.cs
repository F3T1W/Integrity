// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Integrity.Areas.Identity.Data;
using Integrity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Encodings.Web;

namespace Integrity.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private EmailSender _emailSender;

        private readonly IConfiguration _configuration;
        private readonly UserManager<IntegrityUser> _userManager;

        public ForgotPasswordModel(IConfiguration configuration, UserManager<IntegrityUser> userManager, IEmailSender emailSender)
        {
            _configuration = configuration;
            _userManager = userManager;

            _emailSender = new EmailSender();
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme,
                    host: _configuration["DevTunnelSettings:CallbackUrl"]);

                _emailSender.SendEmailAsync(Input.Email, "Welcome to Integrity, again :3", "You can change your password", callbackUrl);

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }

        private async Task SendMail(string callbackUrl)
        {
            try
            {
                using MailMessage message = new();
                message.From = new MailAddress("particular0010abyss@gmail.com");
                message.To.Add(new MailAddress(Input.Email));
                message.Subject = "Welcome to Integrity, again ;3";
                message.IsBodyHtml = true;
                message.Body = $"You can change your account password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";

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
