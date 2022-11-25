using Application.InterfaceRepos;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    public List<Category> GetAllCategories()
    {
        throw new NotImplementedException();
    }

    public Category CreateCategory(Category dto)
    {
        throw new NotImplementedException();
    }

    public Category UpdateCategory(int id, Category dto)
    {
        throw new NotImplementedException();
    }

    public Category DeleteCategory(int id)
    {
        throw new NotImplementedException();
    }
}