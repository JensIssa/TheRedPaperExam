using Domain.Entities;

namespace Application.DTOs;

public class PutProductDTO
{
    public int productId
    {
        get;
        set;
    }
    
    public string ProductName
    {
        get;
        set;
    }

    public string ImageUrl
    {
        get;
        set;
    }

    public string Description
    {
        get;
        set;
    }

    public double Price
    {
        get;
        set;
    }

    public Condition ProductCondition
    {
        get;
        set;
    }
}