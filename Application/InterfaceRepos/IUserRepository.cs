using Domain.Entities;

namespace Application.InterfaceRepos;

public interface IUserRepository
{
    public User GetUserByUsername(string username);
    public User CreateNewUser(User user);
}