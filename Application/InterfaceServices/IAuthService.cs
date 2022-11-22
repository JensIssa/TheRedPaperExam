using Application.DTOs;

namespace Application.InterfaceServices;

public interface IAuthService
{
    public string Register(LoginRegisterDTO dto);
    public string Login(LoginRegisterDTO dto);
}