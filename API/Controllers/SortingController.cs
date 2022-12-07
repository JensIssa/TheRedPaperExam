using Application.InterfaceServices;
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
    [Route("GetAllProductsFromPriceLowToHigh")]
    public List<Product> GetAllProductsFromPrice()
    {
        return _service.SortProductsByPrice();
    }

    [HttpGet]
    [Route("GetAllProductsFromPriceHighToLow")]
    public List<Product> GetAllProductsFromPriceReverse()
    {
        return _service.SortProductsByPriceReverse();
    }

    [HttpGet]
    [Route("GetAllProductsAlphabetSortingA-Z")]
    public List<Product> GetAllProductsAlphabetically()
    {
        return _service.SortProductsAlphabetically();
    }
    
    [HttpGet]
    [Route("GetAllProductsAlphabetSortingZ-A")]
    public List<Product> GetAllProductsAlphabeticallyReverse()
    {
        return _service.SortProductsAlphabeticallyReverse();
    }
}