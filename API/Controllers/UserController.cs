using Application.DTOs;
using Application.InterfaceServices;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Authorize("AdminPolicy")]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }
    /// <summary>
    /// Method used to get a list of all the users by sending a http get request
    /// </summary>
    /// <returns>A list of users</returns>
    [HttpGet]
    public ActionResult<List<User>> GetAllUsers()
    {
       return _service.GetAllUsers(); 
    }
    /// <summary>
    /// Method used to get an User by the username
    /// </summary>
    /// <param name="username">an user's username</param>
    /// <returns>An user</returns>
    [HttpGet]
    [Route("username")]
    public ActionResult<User> GetUserByUsername(string username)
    {
        try
        {
            return _service.GetUserByUsername(username);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound("No user has been found" + username);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
    /// <summary>
    /// Method used to delete a user by sending a http delete request
    /// </summary>
    /// <param name="id">The user's id</param>
    /// <returns>The user is deleted</returns>
    [HttpDelete]
    [Route("{id}")]
    public ActionResult<User> DeleteUser(int id)
    {
        try
        {
            return Ok(_service.DeleteUser(id));
        }
        catch (KeyNotFoundException k)
        {
            return NotFound("No user has been found " + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
    /// <summary>
    /// Method used to update an user by sending a http put request
    /// </summary>
    /// <param name="id">the user's id</param>
    /// <param name="user">The user object</param>
    /// <returns>An updated user</returns>
    [HttpPut]
    [Route("Edit/{id}")]
    public ActionResult<User> UpdateUser( [FromRoute]int id, [FromBody]PutUserDTO user)
    {
        try
        {
            return Ok(_service.UpdateUser(id, user));
        }
        catch (KeyNotFoundException k)
        {
            return NotFound("No user has been found " + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
}