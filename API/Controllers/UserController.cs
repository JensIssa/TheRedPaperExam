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

    [HttpGet]
    public ActionResult<List<User>> GetAllUsers()
    {
       return _service.GetAllUsers(); 
    }

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
            return NotFound("No User has been found" + username);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
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
    [HttpPut]
    [Route("Edit/{id}")]
    public ActionResult<User> UpdateBox( [FromRoute]int id, [FromBody]PutUserDTO box)
    {
        try
        {
            return Ok(_service.UpdateUser(id, box));
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