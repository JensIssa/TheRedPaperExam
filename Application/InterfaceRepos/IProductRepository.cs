using Domain.Entities;

namespace Application.InterfaceRepos;

public interface IProductRepository
{
    public List<Product> GetAllProducts();
    public List<Product> GetAllProductsFromSubcategory(int subcategoryId);

    public List<Product> GetAllProductsFromUser(int userId);

    public Product AddProductToUser(Product dto);

    public Product DeleteProductFromUser(int id);

    public Product getProductById(int productID);
    
    public Product UpdateProduct(int productId, Product dto);

    public List<Product> SetProductsToSold(List<Product> products);
}