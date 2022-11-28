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
    [Route("register")]
    public ActionResult Register(RegisterDTO dto)
    {
        try
        {
            return Ok(_user.CreateUser(dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}