using Application.DTOs;

namespace Application.InterfaceServices;

public interface IAuthService
{
    public string Register(RegisterDTO dto);
    public string Login(LoginDTO dto);
}