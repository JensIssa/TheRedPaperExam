using Application.InterfaceRepos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private  RepositoryDBContext _context;

    public CategoryRepository(RepositoryDBContext context)
    {
        _context = context;
    }

    public List<Category> GetAllCategories()
    {
        return _context.CategoryTable.Include(s => s.SubCategories).ToList();
    }

    public Category CreateCategory(Category category)
    {
        _context.CategoryTable.Add(category);
        _context.SaveChanges();
        return category;
    }

    public Category UpdateCategory(int id, Category dto)
    {
        var categoryToUpdate = _context.CategoryTable.Find(id) ?? throw new KeyNotFoundException("Id to update not found");
        _context.CategoryTable.Update(categoryToUpdate);
        _context.SaveChanges();
        return categoryToUpdate;
    }

    public Category DeleteCategory(int id)
    {
        var categoryToDelete =
            _context.CategoryTable.Find(id) ?? throw new KeyNotFoundException("Id to delete not found");
        _context.CategoryTable.Remove(categoryToDelete);
        _context.SaveChanges();
        return categoryToDelete;
    }
}