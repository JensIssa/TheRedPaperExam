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
}