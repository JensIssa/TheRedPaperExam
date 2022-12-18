using Application.InterfaceServices;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ConditionController
{
    private IConditionService _service;
    
    public ConditionController(IConditionService service)
    {
        _service = service;
    }
    
/// <summary>
/// Gets a list of all the conditions
/// </summary>
/// <returns>a list of condition objects</returns>
    [HttpGet]
    public ActionResult<List<Condition>> GetAllConditions()
    {
        return _service.GetAllConditions();
    }
}