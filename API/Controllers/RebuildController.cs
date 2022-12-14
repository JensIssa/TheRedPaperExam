using Application.InterfaceServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
public class RebuildController: ControllerBase
{
    private IRebuildService _service;

    public RebuildController(IRebuildService service)
    {
        _service = service;
    }
    
    /// <summary>
    /// Method used to create the database by sending a http get request
    /// </summary>
    [HttpGet]
    [Route("CreateDB")]
    public void CreateDB()
    {
        _service.RebuildDB();
    }
    
}