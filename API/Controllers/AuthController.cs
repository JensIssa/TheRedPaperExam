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
    

    public AuthController(IAuthService auth)
    {
        _auth = auth;
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
            return Ok(_auth.Register(dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}