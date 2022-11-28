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

    public Category? Category
    {
        get;
        set;
    }

    public int CategoryID
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