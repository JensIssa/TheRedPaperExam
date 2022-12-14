using Application.DTOs;

namespace Application.InterfaceServices;

public interface IAuthService
{
     
     /// <summary>
     /// This method logs into our program. Everytime a user logs in the bCrypt libary verifies the users token
     /// </summary>
     /// <param name="dto">this dto contains the properties that are used to login to the program</param>
     /// <returns></returns>
     string Login(LoginDTO dto);

     /// <summary>
     /// This method updates an users password
     /// </summary>
     /// <param name="userId">The id of the specific user who's password is getting updated</param>
     /// <param name="dto">this dto contains the properties that are used to update the password</param>
     /// <returns></returns>
     string UpdatePassword(int userId, PutPasswordDTO dto);
     
}