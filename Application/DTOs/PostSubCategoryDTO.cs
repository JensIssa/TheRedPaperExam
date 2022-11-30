using System.Text.Json.Serialization;
using Domain.Entities;

namespace Application.DTOs;

public class PostSubCategoryDTO
{
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
}