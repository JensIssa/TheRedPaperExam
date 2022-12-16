using Application.DTOs;
using Domain.Entities;

namespace Application.InterfaceServices;

public interface IProductService
{
    /// <summary>
    /// This method returns a list of all the product objects
    /// </summary>
    /// <returns>A list of all the product objects</returns>
    public List<Product> GetAllProducts();
    /// <summary>
    /// This method returns a list of all the product objects connected to a specific subcategoryId
    /// </summary>
    /// <param name="subcategoryId">The id of the subcategory which the products are connected to</param>
    /// <returns>a list of all the product objects connected to a specific subcategoryId</returns>
    public List<Product> GetAllProductsFromSubcategory(int subcategoryId);
    /// <summary>
    /// Returns a list of all the product objects connected to a specific userId
    /// </summary>
    /// <param name="userId">The id of the user which the products are connected to</param>
    /// <returns>A list of all the product objects connected to a specific userId</returns>
    public List<Product> GetAllProductsFromUser(int userId);
    /// <summary>
    /// This method adds a product to a specific user
    /// </summary>
    /// <param name="dto">The dto contains all the properties that are used to add a product to a specific user</param>
    /// <returns>The product added to the specific user</returns>
    public Product AddProductToUser(PostProductDTO dto);
    /// <summary>
    /// This method deletes a product connected to a specific user
    /// </summary>
    /// <param name="id">The id of the product which is getting deleted</param>
    /// <returns>the deleted product from a specific user</returns>
    public Product DeleteProductFromUser(int id);
    /// <summary>
    /// This method updates a product connected to a specific user
    /// </summary>
    /// <param name="productId">The id of the product which is getting updated</param>
    /// <param name="dto">The dto contains all the properties used to update a product connected to a specific user</param>
    /// <returns>the updated product</returns>
    public Product UpdateProduct(int productId, PutProductDTO dto);
    
    /// <summary>
    /// This method gets a list of all the products connected to a specific order
    /// </summary>
    /// <param name="orderId">The orderId of the specific order which the products are connected to</param>
    /// <returns>a list of all the products connected to a specific orderId</returns>
    public List<Product> GetProductsByOrderId(int orderId);

}