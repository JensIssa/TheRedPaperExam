using System.Net.Sockets;
using System.Security.Cryptography;
using Application.DTOs;
using Application.InterfaceRepos;
using Domain.Entities;
using FluentValidation;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly RepositoryDBContext _context;


    public UserRepository(RepositoryDBContext context)
    {
        _context = context;
    }
    public User CreateNewUser(User user)
    {
        _context.UserTable.Add(user);
        _context.SaveChanges();
        return user;
    }

    public User GetUserByUsername(string username)
    {
        return _context.UserTable.FirstOrDefault(u => u.Username == username) ??
               throw new KeyNotFoundException("There was no matching username found");
    }

    public List<User> GetAllUsers()
    {
        return _context.UserTable.Where(u => u.AssignedRole == Role.Customer).ToList();
    }

    public User UpdateUser(User user, int id)
    {
        var userToUpdate = _context.UserTable.FirstOrDefault(u => u.Id == id);
        if (userToUpdate.Id == id)
        {
            userToUpdate.Salt = user.Salt;
            userToUpdate.Hash = user.Hash;
            userToUpdate.Email = user.Email;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Location = user.Location;
            userToUpdate.Username = user.Username;
            userToUpdate.PhoneNumber = user.PhoneNumber;
            userToUpdate.BirthDay= user.BirthDay;
            userToUpdate.Id = user.Id;
            _context.Update(userToUpdate);
            _context.SaveChanges();
        }
        return userToUpdate;
    }

    public User DeleteUser(int id)
    {
        var userToDelete = _context.UserTable.Find(id) ?? throw new KeyNotFoundException("Id to delete not found");
        _context.UserTable.Remove(userToDelete);
        _context.SaveChanges();
        return userToDelete;
    }
}