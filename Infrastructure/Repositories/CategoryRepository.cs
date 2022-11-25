using Application.InterfaceRepos;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly RepositoryDBContext _context;

    public CategoryRepository(RepositoryDBContext context)
    {
        _context = context;
    }

    public List<Category> GetAllCategories()
    {
        return _context.CategoryTable.ToList();
    }

    public Category CreateCategory(Category category)
    {
        _context.CategoryTable.Add(category);
        _context.SaveChanges();
        return category;
    }

    public Category UpdateCategory(int id, Category dto)
    {
        throw new NotImplementedException();
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