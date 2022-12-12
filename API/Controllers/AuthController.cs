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
    [HttpPut]
    [Route("UpdatePassword/{id}")]
    public ActionResult UpdatePassword([FromRoute] int id, [FromBody] PutUserDTO dto)
    {
        try
        {
            Console.WriteLine(id);
            Console.WriteLine(dto);
            return Ok(_auth.UpdatePassword(id, dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}