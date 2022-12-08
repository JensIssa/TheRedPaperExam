using Domain.Entities;

namespace Application.DTOs;

public class PutProductDTO
{
    public int Id
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

    public int ProductConditionId
    {
        get;
        set;
    }

    public int SubCategoryId
    {
        get;
        set;
    }
}