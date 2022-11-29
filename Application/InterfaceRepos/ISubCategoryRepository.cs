using Domain.Entities;

namespace Application.InterfaceRepos;

public interface ISubCategoryRepository
{
    List<SubCategory> GetAllSubCategoriesFromCategory(int categoryId);
    SubCategory addSubCategoryToCategory(SubCategory dto);
    SubCategory deleteSubCategoryFromCategory( int subcategoryId);
}