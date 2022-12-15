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
    
    /// <summary>
    /// Method used to get all products by sending a http get request
    /// </summary>
    /// <returns>All products</returns>
    [HttpGet]
    [Route("GetAllProducts")]

    public List<Product> GetAllProducts()
    {
        return _service.GetAllProducts();
    }
    /// <summary>
    /// Method used to get all products from a subcategory by sending a http get request
    /// </summary>
    /// <param name="id">subcategory id</param>
    /// <returns>List of all the products</returns>
    [HttpGet]
    [Route("GetAllProductsFromSub/{id}")]
    public List<Product> GetAllProductsFromSubcategory(int id)
    {
        return _service.GetAllProductsFromSubcategory(id);
    }
    /// <summary>
    /// Method used to get all products from an user by sending a http get request
    /// </summary>
    /// <param name="id">user id</param>
    /// <returns>List of all the products</returns>
    [HttpGet]
    [Route("getProductsFromUser{id}")]
    public List<Product> GetAllProductsFromUser(int id)
    {
        return _service.GetAllProductsFromUser(id);
    }

    /// <summary>
    /// Method used to create a product by sending a http post request
    /// </summary>
    /// <param name="dto">Dto containing all the properties to create a product</param>
    /// <returns>A new product</returns>
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
    /// <summary>
    /// Method used to delete a product by sending a http delete request
    /// </summary>
    /// <param name="id">The product id</param>
    /// <returns>Product is deleted</returns>
    [HttpDelete]
    [Route("{id}")]
    public ActionResult<Product> DeleteProduct(int id)
    {
        try
        {
            return Ok(_service.DeleteProductFromUser(id));
        }
        catch (KeyNotFoundException k)
        {
            return NotFound("No product has been found " + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
    /// <summary>
    /// Method used to edit a product by sending a http put request
    /// </summary>
    /// <param name="Id">product id</param>
    /// <param name="dto">dto containing all the properties to update a product</param>
    /// <returns>An updated product</returns>
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
    /// <summary>
    /// Method used to get a list of all the products by orderId by sending a http get request
    /// </summary>
    /// <param name="orderId">the id of the order</param>
    /// <returns>A list of all the products</returns>
    [HttpGet]
    [Route("GetAllProductsByOrderId/{orderId}")]
    public List<Product> GetAllProductsByOrderId(int orderId)
    {
        return _service.GetProductsByOrderId(orderId);
    }
    
}