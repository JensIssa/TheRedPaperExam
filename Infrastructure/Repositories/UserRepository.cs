using Application.InterfaceRepos;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly RepositoryDBContext _context;

    public UserRepository(RepositoryDBContext context)
    {
        _context = context;
    }

    public User GetUserByUsername(string username)
    {
        return _context.UserTable.FirstOrDefault(u => u.Username == username) ??
               throw new KeyNotFoundException("There was no matching username found");
    }

    public User CreateNewUser(User user)
    {
        _context.UserTable.Add(user);
        _context.SaveChanges();
        return user;
    }
}