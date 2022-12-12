using Domain.Entities;

namespace Application.InterfaceRepos;

public interface IProductRepository
{
    List<Product> GetAllProducts();
    List<Product> GetAllProductsFromSubcategory(int subcategoryId);

    List<Product> GetAllProductsFromUser(int userId);

    Product AddProductToUser(Product dto);

    Product DeleteProductFromUser(int id);

    Product getProductById(int productID);
    
    Product UpdateProduct(int productId, Product dto);

    List<Product> SetProductsToSold(List<Product> products);
    
    public List<Product> GetProductsById(List<int> productIds);
    public List<Product> GetProductsByOrderId(int orderId);


}