using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.ComponentModel.DataAnnotations;

namespace BTS.Utilities.EmailSender
{
    public class EmailSender:IEmailSender
    {
        private readonly string? _sendGridAPIkey;
        private readonly string? _from;
        public EmailSender(IConfiguration config)
        {
            _sendGridAPIkey = System.Environment.GetEnvironmentVariable("SEND_GRID_API_KEY");
            _from = System.Environment.GetEnvironmentVariable("SUPER_ADMIN_EMAIL");
        }

        public async Task SendEmailAsync (string email, string subject, string htmlMessage)
        {

            try
            {
                var sendGridClient = new SendGridClient(_sendGridAPIkey);
                var from = new EmailAddress("developercsharp6@gmail.com", "Password recovery");
                var to = new EmailAddress(email);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
                var response = await sendGridClient.SendEmailAsync(msg);
                if (!response.IsSuccessStatusCode)
                {
                    var errorBody = await response.Body.ReadAsStringAsync();
                    Console.WriteLine($"SendGrid Error [{response.StatusCode}]: {errorBody}");
                    throw new Exception($"SendGrid failed: {errorBody}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email to {email}: {ex.Message}");
            }

        }
    }
}
