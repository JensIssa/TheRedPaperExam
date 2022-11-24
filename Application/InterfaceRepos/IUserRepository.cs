using Domain.Entities;

namespace Application.InterfaceRepos;

public interface IUserRepository
{
    public User CreateNewUser(User user);
    public User GetUserByUsername(string username);
    public List<User> GetAllUsers();
    public User UpdateUser(User user, int id);
    public User DeleteUser(int id);

}