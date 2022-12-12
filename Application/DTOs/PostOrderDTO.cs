using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Entities;

namespace Application.DTOs;

public class PostOrderDTO
{
    public int UserId { get; set; }
    
    public List<int> ProductsId
    {
        get; set;
    }
    public List<Product>? Products { get; set; }
}