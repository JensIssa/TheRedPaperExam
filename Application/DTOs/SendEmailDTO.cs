namespace Application.DTOs;

public class SendEmailDTO
{
    public string From { get; set; }
    
    public string Issue { get; set; }
    
    public string Subject { get; set; }
    
    public string Body { get; set; }
}