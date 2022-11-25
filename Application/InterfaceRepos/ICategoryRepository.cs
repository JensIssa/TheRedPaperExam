using Domain.Entities;

namespace Application.InterfaceRepos;

public interface ICategoryRepository
{
    public List<Category> GetAllCategories();
    public Category CreateCategory(Category dto);
    public Category UpdateCategory(int id, Category dto);
    public Category DeleteCategory(int id);
}