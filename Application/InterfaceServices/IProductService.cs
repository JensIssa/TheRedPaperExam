using Application.DTOs;
using Domain.Entities;

namespace Application.InterfaceServices;

public interface IProductService
{
    public List<Product> GetAllProductsFromSubcategory(int subcategoryId);

    public List<Product> GetAllProductsFromUser(int userId);

    public Product AddProductToUser(PostProductDTO dto);

    public Product DeleteProductFromUser(int productId);

    public Product UpdateProduct(int productId, PutProductDTO dto);

    public Product getProductById(int productID);
}