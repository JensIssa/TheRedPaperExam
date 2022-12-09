namespace Domain.Entities;

public class Order
{
    public int Id
    {
        get;
        set;
    }
    
    public User user { get; set; }
    
    public int userId { get; set; }
    
    public List<Product> Products { get; set; }
}