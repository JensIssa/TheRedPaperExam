using Application.DTOs;
using Application.InterfaceServices;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class SubCategoryController : ControllerBase
{
    private ISubCategoryService _subCategoryService;

    public SubCategoryController(ISubCategoryService subCategoryService)
    {
        _subCategoryService = subCategoryService;
    }
    /// <summary>
    /// Method used to get a list of all subcategories from a category by sending a http get request
    /// </summary>
    /// <param name="id">The category id</param>
    /// <returns>A list of subcategories</returns>
    [HttpGet]
    [Route("GetAllSubsFromCategories/{id}")]
    public List<SubCategory> GetAllSubCategoriesFromCategory(int id)
    {
        return _subCategoryService.GetAllSubCategoriesFromCategory(id);
    }
    /// <summary>
    /// Method used to get a list of all the subcategories by sending a http get request
    /// </summary>
    /// <returns>A list of all the subcategories</returns>
    [HttpGet]
    [Route("GetAllSubs")]
    public List<SubCategory> getAllSubCategories()
    {
        return _subCategoryService.GetAllSubCategories();
    }
    /// <summary>
    /// Method used to create a new subcategory by sending a http post request
    /// </summary>
    /// <param name="dto">The dto containing all the properties to create a new subcategory</param>
    /// <returns>a new subcategory</returns>
    [HttpPost]
    public ActionResult<Category> CreateSubCategory(PostSubCategoryDTO dto)
    {
        try
        {
            SubCategory result = _subCategoryService.AddSubCategoryToCategory(dto);
            return Created("SubCategory" + result.Id, result);
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