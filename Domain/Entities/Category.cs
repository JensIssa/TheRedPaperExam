namespace Domain.Entities;

public class Category
{
    public int Id
    {
        get;
        set;
    }

    public string CategoryName
    {
        get;
        set;
    }

    public List<SubCategory> SubCategories
    {
        get;
        set;
    }
}