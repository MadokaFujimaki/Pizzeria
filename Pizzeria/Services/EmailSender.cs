using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            Debug.WriteLine($"To: {email} Subject: {subject} Message: {message}"); //Det skrivs meddelandet på debug fönstret i visual studio.
            return Task.CompletedTask;
        }
    }
}
