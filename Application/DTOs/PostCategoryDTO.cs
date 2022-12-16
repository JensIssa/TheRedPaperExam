using System.Text.Json.Serialization;
using Domain.Entities;

namespace Application.DTOs;

public class PostCategoryDTO
{
    public string CategoryName { get; set; }
    
    [JsonIgnore]
    public List<SubCategory>? SubCategories { get; set; }
}