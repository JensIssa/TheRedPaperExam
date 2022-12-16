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
    /// <summary>
    /// Method used to get a list of all the orders by sending a http get request
    /// </summary>
    /// <returns>A list of orders</returns>
    [HttpGet]
    [Route("GetAllOrders")]

    public List<Order> GetAllOrders()
    {
        return _service.GetAllOrders();
    }
    /// <summary>
    /// Method used to create an order by sending a http post request
    /// </summary>
    /// <param name="dto">The dto containing all properties used to create an order</param>
    /// <returns>An order</returns>
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
    /// <summary>
    /// Method used to get all the orders from an user by sending a http get request
    /// </summary>
    /// <param name="userId">the user's id</param>
    /// <returns>a list of orders</returns>
    [HttpGet]
    [Route("GetAllOrdersFromUser/{userId}")]

    public List<Order> GetAllOrdersFromUser(int userId)
    {
        return _service.GetAllOrdersByUser(userId);
    }
}