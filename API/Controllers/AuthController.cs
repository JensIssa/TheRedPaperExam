using Application.DTOs;
using Application.InterfaceServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController: ControllerBase
{

    private readonly IAuthService _auth;
    private readonly IUserService _user;
    
    
    public AuthController(IAuthService auth, IUserService user)
    {
        _auth = auth;
        _user = user;
    }
    /// <summary>
    /// This method is used to login by sending a http post request
    /// </summary>
    /// <param name="dto">The dto containing the properties used to login</param>
    /// <returns>A succesfull login</returns>
    [HttpPost]
    [Route("login")]
    public ActionResult Login(LoginDTO dto)
    {
        try
        {
            return Ok(_auth.Login(dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    /// <summary>
    /// This method is used to register a customer by sending a http post request
    /// </summary>
    /// <param name="dto">The dto containing the properties used to register an customer</param>
    /// <returns>A new costumer</returns>
    [HttpPost]
    [Route("RegisterUser")]
    public ActionResult RegisterUser(RegisterDTO dto)
    {
        try
        {
            return Ok(_user.CreateCustomer(dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    /// <summary>
    /// This method is used to register an admin by sending a http post request
    /// </summary>
    /// <param name="dto">The dto containing the properties used to register an admin</param>
    /// <returns>A new admin</returns>
    [HttpPost]
    [Route("RegisterAdmin")]
    public ActionResult RegisterAdmin(RegisterDTO dto)
    {
        try
        {
            return Ok(_user.CreateAdmin(dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    /// <summary>
    /// This method is used to update a password by sending a put request
    /// </summary>
    /// <param name="id">the users id</param>
    /// <param name="dto">The dto containing the properties used to update a password</param>
    /// <returns>An updated password</returns>
    [HttpPut]
    [Route("UpdatePassword/{id}")]
    public ActionResult UpdatePassword([FromRoute] int id, [FromBody] PutPasswordDTO dto)
    {
        try
        {
            return Ok(_auth.UpdatePassword(id, dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}