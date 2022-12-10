using Application.DTOs;
using Application.InterfaceServices;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private IOrderService _service;

    public OrderController(IOrderService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("GetAllOrders")]

    public List<Order> GetAllOrders()
    {
        return _service.getAllOrders();
    }

    [HttpPost]
    public ActionResult<Order> CreateOrder(PostOrderDTO dto)
    {
        Console.WriteLine(dto);
        try
        {
            Order result = _service.CreateOrder(dto);
            return Created("Order" + result.Id, result);
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
    [Route("GetAllOrdersFromUser/{id}")]

    public List<Order> GetAllOrdersFromUser(int id)
    {
        return _service.GetAllOrdersByUser(id);
    }
    
}