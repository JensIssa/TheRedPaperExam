using Application.InterfaceServices;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ConditionController
{
    private IConditionService _service;

    public ConditionController(IConditionService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<Condition>> GetAllCategories()
    {
        return _service.getAllConditions();
    }
}