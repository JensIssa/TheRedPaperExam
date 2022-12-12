using Domain.Entities;

namespace Application.InterfaceRepos;

public interface ICategoryRepository
{
    List<Category> GetAllCategories();
    Category CreateCategory(Category category);
    Category UpdateCategory(int id, Category category);
    Category DeleteCategory(int id);
}