using Application.InterfaceRepos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private RepositoryDBContext _context;

    public ProductRepository(RepositoryDBContext context)
    {
        _context = context;
    }

    public List<Product> GetAllProductsFromSubcategory(int subcategoryId)
    {
        return _context.ProductTable.Where(i => i.SubCategoryID == subcategoryId).ToList();
    }

    public List<Product> GetAllProductsFromUser(int userId)
    {
        return _context.ProductTable.Where(i => i.UserId == userId).Include(p => p.ProductCondition).ToList();
    }

    public Product AddProductToUser(Product dto)
    {
        _context.Add(dto);
        _context.SaveChanges();
        return dto;
    }

    public Product DeleteProductFromUser(int productId)
    {
        var productToDelete = _context.ProductTable.Find(productId) ?? throw new KeyNotFoundException("Id was not found to delete");
        _context.ProductTable.Remove(productToDelete);
        _context.SaveChanges();
        return productToDelete;
    }

    public Product getProductById(int productID)
    {
        return _context.ProductTable.FirstOrDefault(p => p.Id == productID) ?? throw new KeyNotFoundException("There was no matching id found");

        
    }
    public Product UpdateProduct(int productId, Product dto)
    {
        var productToUpdate =
            _context.ProductTable.Find(productId) ?? throw new KeyNotFoundException("Id to delete not found");
        _context.ProductTable.Update(productToUpdate);
        _context.SaveChanges();
        return productToUpdate; 
    }
}