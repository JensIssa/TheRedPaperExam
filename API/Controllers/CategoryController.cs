using Application.DTOs;
using Application.InterfaceServices;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<Category>> GetAllCategories()
    {
        return _service.GetAllCategories();
    }

    [HttpPost]
    public ActionResult<Category> CreateCategory(PostCategoryDTO dto)
    {
        try
        {
            Category result = _service.CreateCategory(dto);
            return Created("Category" + result.Id, result);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut]
    [Route("Edit/{Id}")]
    public ActionResult<Category> UpdateCategory([FromRoute] int Id, [FromBody] PutCategoryDTO dto)
    {
        try
        {
            return Ok(_service.UpdateCategory(Id, dto));
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    [Route("Delete/{Id}")]
    public ActionResult<Category> DeleteCategory([FromRoute]int id)
    {
        try
        {
            return Ok(_service.DeleteCategory(id));
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}