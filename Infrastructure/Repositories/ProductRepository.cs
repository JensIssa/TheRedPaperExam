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

    public List<Product> GetAllProducts()
    {
        return _context.ProductTable.Where(p => p.isSold == false).Include(p=>p.User).Include(p=>p.ProductCondition).ToList();
    }

    public List<Product> GetAllProductsFromSubcategory(int subcategoryId)
    {
        return _context.ProductTable.Where(i => i.SubCategoryID == subcategoryId).Where(p => p.isSold == false).Include(p => p.User).Include(p => p.ProductCondition).ToList();
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

    public Product DeleteProductFromUser(int id)
    {
        var productToDelete = _context.ProductTable.Find(id) ?? throw new KeyNotFoundException("Id was not found to delete");
        _context.ProductTable.Remove(productToDelete);
        _context.SaveChanges();
        return productToDelete;
    }
    public Product UpdateProduct(int productId, Product dto)
    {
        var productToUpdate = _context.ProductTable.FirstOrDefault(p => p.Id == productId) ?? throw new KeyNotFoundException("Id to update not found");
        if (productId == productToUpdate.Id)
        {
            productToUpdate.Description = dto.Description ;
            productToUpdate.Price = dto.Price;
            productToUpdate.ImageUrl = dto.ImageUrl ;
            productToUpdate.ProductCondition = dto.ProductCondition;
            productToUpdate.ProductName = dto.ProductName ;
            productToUpdate.ProductConditionId = dto.ProductConditionId;
            productToUpdate.SubCategoryID = dto.SubCategoryID;
            _context.Update(productToUpdate);
            _context.SaveChanges();
        }
        return productToUpdate; 
    }

    public List<Product> SetProductsToSold(List<Product> products)
    {
        products.ForEach(p =>
        {
            p.isSold = true;
        });
        _context.UpdateRange(products);
       _context.SaveChanges();
        return products;
    }

    public List<Product> GetProductsByOrderId(int orderId)
    {
        return _context.ProductTable.Where(p => p.OrderId == orderId).ToList();
    }
    public List<Product> GetProductsById(List<int> productIds)
    {
        return _context.ProductTable.Where(p => productIds.Any(x => x.Equals(p.Id))).ToList();
    }
}