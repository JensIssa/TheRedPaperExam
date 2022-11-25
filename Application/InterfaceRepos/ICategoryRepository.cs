using Domain.Entities;

namespace Application.InterfaceRepos;

public interface ICategoryRepository
{
    public List<Category> GetAllCategories();
    public Category CreateCategory(Category category);
    public Category UpdateCategory(int id, Category category);
    public Category DeleteCategory(int id);
}