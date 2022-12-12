﻿using Application.DTOs;
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
    [Route("getProductsFromUser{id}")]
    public List<Product> GetAllProductsFromUser(int id)
    {
        return _service.GetAllProductsFromUser(id);
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
            return _service.GetProductById(id);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound("No product has been found" + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
    
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
    
    [HttpGet]
    [Route("GetAllProductsByOrderId/{orderId}")]
    public List<Product> GetAllProductsByOrderId(int orderId)
    {
        return _service.GetProductsByOrderId(orderId);
    }
    
}