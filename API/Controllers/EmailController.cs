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
    
    [HttpPost]
    public IActionResult SendEmail(SendEmailDTO request)
    {
        _emailService.sendEmail(request);
        return Ok();
    }
}