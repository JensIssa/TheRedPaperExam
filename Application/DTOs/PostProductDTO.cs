using System.Text.Json.Serialization;
using Domain.Entities;

namespace Application.DTOs;

public class PostProductDTO
{
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
    
    public int SubCategoryID
    {
        get;
        set;
    }
    
    public int UserId
    {
        get;
        set;
    }
}