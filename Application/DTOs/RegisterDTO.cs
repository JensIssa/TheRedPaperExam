namespace Application.DTOs;

public class RegisterDTO
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }
    public string Email { get; set;}
    public int PhoneNumber { get; set; }
    public string location { get; set; }
}