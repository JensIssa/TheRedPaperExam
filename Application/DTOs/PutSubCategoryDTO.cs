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

    public int? CategoryID
    {
        get;
        set;
    }
}