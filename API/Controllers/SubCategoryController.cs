using Application.DTOs;
using Application.InterfaceServices;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class SubCategoryController : ControllerBase
{
    private ISubCategoryService _subCategoryService;

    public SubCategoryController(ISubCategoryService subCategoryService)
    {
        _subCategoryService = subCategoryService;
    }
    
    [HttpGet]
    [Route("GetAllSubsFromCategories/{id}")]
    public List<SubCategory> GetAllSubCategoriesFromCategory(int id)
    {
        return _subCategoryService.GetAllSubCategoriesFromCategory(id);
    }

    [HttpGet]
    [Route("GetAllSubs")]
    public List<SubCategory> getAllSubCategories()
    {
        return _subCategoryService.getAllSubCategories();
    }

    [HttpPost]
    public ActionResult<Category> CreateSubCategory(PostSubCategoryDTO dto)
    {
        try
        {
            SubCategory result = _subCategoryService.addSubCategoryToCategory(dto);
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
    
}