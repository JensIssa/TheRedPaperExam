using Application.DTOs;
using Application.InterfaceServices;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Authorize]
[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }
    
    /// <summary>
    /// Method used to get a list of Categories by sending a http get request
    /// </summary>
    /// <returns>A list of categories</returns>
    [HttpGet]
    public ActionResult<List<Category>> GetAllCategories()
    {
        return _service.GetAllCategories();
    }
    /// <summary>
    /// Method used to create a category by sending a http post request
    /// </summary>
    /// <param name="dto">The dto containing all the properties to create a category</param>
    /// <returns>A category object</returns>
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
    /// <summary>
    /// A method used to update a category by sending a http put request
    /// </summary>
    /// <param name="id">The category id</param>
    /// <param name="dto">The dto containing all the properties used to update a category</param>
    /// <returns>An updated category object</returns>
    [HttpPut]
    [Route("Edit/{id}")]
    public ActionResult<Category> UpdateCategory([FromRoute] int id, [FromBody] PutCategoryDTO dto)
    {
        try
        {
            return Ok(_service.UpdateCategory(id, dto));
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
    /// <summary>
    /// Method used to delete a category by sending a http delete request
    /// </summary>
    /// <param name="id">the category id</param>
    /// <returns>A deleted category object</returns>
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