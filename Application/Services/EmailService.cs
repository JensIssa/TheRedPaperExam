using System.Net;
using System.Net.Mail;
using Application.DTOs;
using Application.Helpers;
using Application.InterfaceServices;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Application.Services;

public class EmailService: IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public void sendEmail(SendEmailDTO dto)
    {
        using (MailMessage email = new MailMessage())
        {
            email.From = new MailAddress(dto.From);
            email.To.Add(new MailAddress("TheRedPaperExam@gmail.com"));
            email.Subject = dto.Subject;
            email.SubjectEncoding = System.Text.Encoding.UTF8;
            email.Body = "<p>Email: " + dto.From + "</p>" + "<p>Issue: " + dto.Issue + "</p>" + "<p>Message: " + dto.Body + "</p>";
            email.BodyEncoding = System.Text.Encoding.UTF8;
            email.IsBodyHtml = true;
            using (SmtpClient client = new SmtpClient(_config.GetSection("Host").Value, 587))
            {
                client.Credentials = new NetworkCredential(_config.GetSection("Email").Value,_config.GetSection("Password").Value);
                client.EnableSsl = true;
                client.Send(email);
            }
        }
    }
}