using Application.DTOs;
using Domain.Entities;

namespace Application.InterfaceServices;

public interface IUserService
{
    /// <summary>
    /// This method returns an User by the username
    /// </summary>
    /// <param name="username">The username of the user</param>
    /// <returns>returns a specific user by their username</returns>
    public User GetUserByUsername(string username);
    /// <summary>
    /// This method returns a list of all the users (customers)
    /// </summary>
    /// <returns>Returns a list of all the customers</returns>
    public List<User> GetAllUsers();
    /// <summary>
    /// This method creates a new user with the admin role
    /// </summary>
    /// <param name="dto">the dto contains all the properties used to create an admin</param>
    /// <returns>an user with the role admin</returns>
    public User CreateAdmin(  RegisterDTO dto);

    /// <summary>
    /// This method creates a new user with the customer role
    /// </summary>
    /// <param name="dto">the dto contains all the properties used to create a customer</param>
    /// <returns>an user with the role customer</returns>
    public User CreateCustomer( RegisterDTO dto);
    /// <summary>
    /// This method updates a specific user
    /// </summary>
    /// <param name="id">The id of the specific user to be updated </param>
    /// <param name="putUserDto">The dto containing the properties used to update an user</param>
    /// <returns>an updated user object</returns>
    public User UpdateUser(int id, PutUserDTO putUserDto);
    
    /// <summary>
    /// This method deletes a specific user
    /// </summary>
    /// <param name="id">The id of the specific user to be deleted</param>
    /// <returns>the user object is deleted</returns>
    public User DeleteUser(int id);
    
    
}