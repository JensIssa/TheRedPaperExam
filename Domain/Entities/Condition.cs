namespace Domain.Entities;

public class Condition
{
    
    public int Id { get; set; }
    
    public string Name { get; set;  }
    
    public List<Product>? Products { get; set; }
}