using Domain.Entities;

namespace Application.InterfaceRepos;

public interface IUserRepository
{
    /// <summary>
    /// This method creates a new user to the database
    /// </summary>
    /// <param name="user">The user object</param>
    /// <returns>a new user object</returns>
    public User CreateNewUser(User user);
    /// <summary>
    /// This method gets a specific user by its username from the database
    /// </summary>
    /// <param name="username">the users username</param>
    /// <returns>An user object</returns>
    public User GetUserByUsername(string username);
    /// <summary>
    /// The method returns a list of all the user objects from the database
    /// </summary>
    /// <returns>A list of all the user objects</returns>
    public List<User> GetAllUsers();
    /// <summary>
    /// This method updates a specific user in the database
    /// </summary>
    /// <param name="user">The user object</param>
    /// <param name="id">The id of the user object</param>
    /// <returns>An updated user in the database</returns>
    public User UpdateUser(User user, int id);
    /// <summary>
    /// This method deletes a specific user object from the database
    /// </summary>
    /// <param name="id">The id of a user object</param>
    /// <returns>Deletes an user from the database</returns>
    public User DeleteUser(int id);

    /// <summary>
    /// This method updates an user object's password in the database
    /// </summary>
    /// <param name="userId">the id of the user object</param>
    /// <param name="user">the user object</param>
    /// <returns>an updated password</returns>
    User UpdateUserPassword(int userId, User user);

    /// <summary>
    /// This method gets an user object by its id from the database
    /// </summary>
    /// <param name="id">the user object's id</param>
    /// <returns>A user object</returns>
    User GetUserById(int id);
}