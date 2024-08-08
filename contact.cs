using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class HomeController : Controller
{
    [HttpPost]
    public async Task<IActionResult> SendEmail(string name, string email, string message)
    {
        var mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(email);
        mailMessage.To.Add("your-email@example.com");
        mailMessage.Subject = $"Contact Form Submission from {name}";
        mailMessage.Body = $"Name: {name}\nEmail: {email}\n\nMessage:\n{message}";
        
        using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
        {
            smtpClient.Credentials = new NetworkCredential("your-email@gmail.com", "your-email-password");
            smtpClient.EnableSsl = true;
            
            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                ViewBag.Message = "Email sent successfully!";
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error while sending email: {ex.Message}";
            }
        }
        
        return View("Contact");
    }

    public IActionResult Contact()
    {
        return View();
    }
}
