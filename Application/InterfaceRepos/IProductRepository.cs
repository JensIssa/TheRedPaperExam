using Domain.Entities;

namespace Application.InterfaceRepos;

public interface IProductRepository
{
    
    /// <summary>
    /// This method returns a list of all the product objects from the database
    /// </summary>
    /// <returns>returns a list of all the products</returns>
    List<Product> GetAllProducts();
    
    /// <summary>
    /// This method returns a list of all the products connected to a specific subcategory from the database
    /// </summary>
    /// <param name="subcategoryId">The id of the subcategory</param>
    /// <returns>A list of all products connected to a specific subcategory</returns>
    List<Product> GetAllProductsFromSubcategory(int subcategoryId);
    /// <summary>
    /// This method returns a list of all the products connected to a specific user from the database
    /// </summary>
    /// <param name="userId">the id of the user</param>
    /// <returns>A list of all the products connected to a specific user</returns>
    
    List<Product> GetAllProductsFromUser(int userId);
    /// <summary>
    /// This method creates a product to a specific user to the database
    /// </summary>
    /// <param name="dto">The dto containing all the properties to create a product to a specific user</param>
    /// <returns>a product created to a specific user</returns>
    Product AddProductToUser(Product dto);
    
    /// <summary>
    /// This method deletes a product from a specific user from the database
    /// </summary>
    /// <param name="id">The id of the product object to be deleted</param>
    /// <returns>A deleted product object</returns>
    Product DeleteProductFromUser(int id);

    /// <summary>
    /// This method returns a Product object by its id from the database
    /// </summary>
    /// <param name="productID">The id of the product object</param>
    /// <returns>A product object</returns>
    Product getProductById(int productID);
    
    /// <summary>
    /// This method updates a product object by its id in the database
    /// </summary>
    /// <param name="productId">The id of the product object to be updated</param>
    /// <param name="dto">The dto containing all the properties to update a product object</param>
    /// <returns>An updated product object</returns>
    Product UpdateProduct(int productId, Product dto);
    
    /// <summary>
    /// This method sets a list of products to sold in the database
    /// </summary>
    /// <param name="products">A product object</param>
    /// <returns>A product object sat to sold</returns>
    List<Product> SetProductsToSold(List<Product> products);
    /// <summary>
    /// This method gets a list of product objects by ids from the database
    /// </summary>
    /// <param name="productIds">the product ids</param>
    /// <returns>A list of products by ids from the database</returns>
    public List<Product> GetProductsById(List<int> productIds);
    /// <summary>
    /// This method returns a list of products by orderId from the database
    /// </summary>
    /// <param name="orderId">the order's id</param>
    /// <returns>returns a list of product by orderId</returns>
    public List<Product> GetProductsByOrderId(int orderId);


}