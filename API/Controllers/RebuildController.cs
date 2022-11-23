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
    
    [HttpGet]
    [Route("CreateDB")]
    public void CreateDB()
    {
        _service.RebuildDB();
    }
    
}