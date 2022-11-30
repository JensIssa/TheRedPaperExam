using System.Text.Json.Serialization;

namespace Domain.Entities;

public class SubCategory
{
    public int Id
    {
        get;
        set;
    }

    public string SubName
    {
        get;
        set;
    }

    public int? CategoryID
    {
        get;
        set;
    }

    public List<Product>? Products
    {
        get;
        set;
    }
}