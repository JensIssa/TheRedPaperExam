using Application.DTOs;

namespace Application.InterfaceServices;

public interface IAuthService
{
    public string Login(LoginDTO dto);
}