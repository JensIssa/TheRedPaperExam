using Application.DTOs;

namespace Application.InterfaceServices;

public interface IEmailService
{
    /// <summary>
    /// This method sends an email
    /// </summary>
    /// <param name="dto">The dto contains the properties used to send an e-mail</param>
    void sendEmail(SendEmailDTO dto);
}