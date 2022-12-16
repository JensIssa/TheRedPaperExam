using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Order
{
    public int Id
    {
        get;
        set;
    }
    [JsonIgnore]
    public User? User { get; set; }
    
    public int UserId { get; set; }
    
    public List<Product>? Products { get; set; }
}