using System.Text.Json.Serialization;
using Domain.Entities;

namespace Application.DTOs;

public class PutSubCategoryDTO
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
    
    [JsonIgnore]
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