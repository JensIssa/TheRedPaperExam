using Domain.Entities;

namespace Application.DTOs;

public class PostSubCategoryDTO
{
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
    public int? categoryID
    {
        get;
        set;
    }
}