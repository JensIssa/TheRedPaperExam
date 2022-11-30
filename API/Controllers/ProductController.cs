using Application.DTOs;
using Application.InterfaceServices;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public List<Product> GetAllProductsFromSubcategory(int subcategoryId)
    {
        return _service.GetAllProductsFromSubcategory(subcategoryId);
    }
    

    [HttpPost]
    public ActionResult<Product> CreateProduct(PostProductDTO dto)
    {
        try
        {
            Product result = _service.AddProductToUser(dto);
            return Created("Product" + result.Id, result);
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