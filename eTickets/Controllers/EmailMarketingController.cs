namespace eTickets.Controllers;

using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

public class EmailMarketingController : Controller
{
    private readonly EmailService _emailService;

    public EmailMarketingController(EmailService emailService)
    {
        _emailService = emailService;
    }

    // Display form to enter email + full name
    public IActionResult SendMarketingEmail()
    {
        return View();
    }

    // Get data from form and send email
    [HttpPost]
    public async Task<IActionResult> SendMarketingEmail(EmailMarketingModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model); // If there is an error, return the form with the error message
        }

        string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Templates/EmailTemplate.html");
        string link = "https://your-marketing-link.com"; // Promotional link

        string emailContent = await _emailService.LoadEmailTemplateAsync(templatePath, model.Name, link);
        await _emailService.SendEmailAsync(model.Email, "Special offer for you!", emailContent);

        ViewBag.Message = "Email sent successfully!";
        return View();
    }
}