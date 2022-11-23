using Application.DTOs;
using Domain.Entities;

namespace Application.InterfaceServices;

public interface IUserService
{
    public User CreateNewUser(RegisterDTO postDto);
    public User GetUserByUsername(string username);
    public List<User> GetAllUsers();
    public User UpdateUser(int id, PutUserDTO putUserDto);
    public User DeleteUser(int id);
}