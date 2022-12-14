using Application.DTOs;

namespace Application.InterfaceServices;

public interface IEmailService
{
    void sendEmail(SendEmailDTO dto);
}