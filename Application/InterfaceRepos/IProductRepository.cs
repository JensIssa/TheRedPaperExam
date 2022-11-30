using Domain.Entities;

namespace Application.InterfaceRepos;

public interface IProductRepository
{
    public List<Product> GetAllProductsFromSubcategory(int subcategoryId);

    public List<Product> GetAllProductsFromUser(int userId);

    public Product AddProductToUser(Product dto);

    public Product DeleteProductFromUser(int productId);

    public Product getProductById(int productID);

    public Product UpdateProduct(int productId, Product dto);
}