using Domain.Entities;

namespace Application.DTOs;

public class PostOrderDTO
{
    public int userId { get; set; }
    
    public List<Product> Products 
    { 
        get; 
        set; 
    }
}