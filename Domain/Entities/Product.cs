using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Product
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
    
    public Condition? ProductCondition { get; set; }
    
    public int ProductConditionId { get; set; }
    
    public Boolean isSold { get; set; }
    public User? user { get; set; }
    
    public Order? Order { get; set; }
    
    public int? OrderId { get; set; }
    
    
    
}