﻿using Application.InterfaceServices;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class SortingController
{
    private ISortingService _service;

    public SortingController(ISortingService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("GetAllProductsFromPrice")]
    public List<Product> GetAllProductsFromPrice()
    {
        return _service.SortProductsByPrice();
    }
}