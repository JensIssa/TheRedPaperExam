using Application.DTOs;
using Application.InterfaceServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailController: ControllerBase
{
    private IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }
    /// <summary>
    /// Method used to send an email by sending a http post request
    /// </summary>
    /// <param name="email"></param>
    /// <returns>sends an email</returns>
    [HttpPost]
    public IActionResult SendEmail(SendEmailDTO email)
    {
        _emailService.sendEmail(email);
        return Ok();
    }
}