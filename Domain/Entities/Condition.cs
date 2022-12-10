using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Condition
{
    
    public int Id { get; set; }
    
    public string Name { get; set;  }
    
    [JsonIgnore]
    public List<Product>? Products { get; set; }
}