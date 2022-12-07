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
    [Route("GetAllProducts")]

    public List<Product> GetAllProducts()
    {
        return _service.GetAllProducts();
    }
    
    [HttpGet]
    [Route("GetAllProductsFromSub/{id}")]
    public List<Product> GetAllProductsFromSubcategory(int id)
    {
        return _service.GetAllProductsFromSubcategory(id);
    }
    
    [HttpGet]
    [Route("getProductsFromUser")]
    public List<Product> GetAllProductsFromUser(int userId)
    {
        return _service.GetAllProductsFromUser(userId);
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
    [HttpGet]
    [Route("id")]
    public ActionResult<Product> GetProductById(int id)
    {
        try
        {
            return _service.getProductById(id);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound("No Product has been found" + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
    
    [HttpDelete]
    [Route("Delete/{Id}")]
    public ActionResult<Product> DeleteProduct([FromRoute]int id)
    {
        try
        {
            return Ok(_service.DeleteProductFromUser(id));
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
    public ActionResult<Product> UpdateProduct([FromRoute] int Id, [FromBody] PutProductDTO dto)
    {
        try
        {
            return Ok(_service.UpdateProduct(Id, dto));
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