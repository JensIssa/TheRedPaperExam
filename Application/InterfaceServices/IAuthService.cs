using Application.DTOs;

namespace Application.InterfaceServices;

public interface IAuthService
{
     string Login(LoginDTO dto);

     string UpdatePassword(int userId, PutPasswordDTO dto);
     
}