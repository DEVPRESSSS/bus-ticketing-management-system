using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace BTS.Utilities.EmailSender
{
    public class EmailSender:IEmailSender
    {

        public async Task SendEmailAsync (string subject, string body, string htmlMessage)
        {
            await Task.CompletedTask;
        }
    }
}
