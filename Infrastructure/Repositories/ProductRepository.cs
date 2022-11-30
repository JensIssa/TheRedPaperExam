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
        return _context.ProductTable.Where(i => i.SubCategoryID == subcategoryId).Include
            (x => x.SubCategory).ToList();
    }

    public List<Product> GetAllProductsFromUser(int userId)
    {
        return _context.ProductTable.Where(i => i.userId == userId).Include(x => x.user).ToList();
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

    public Product UpdateProduct(int productId, Product dto)
    {
        throw new NotImplementedException();
    }
}