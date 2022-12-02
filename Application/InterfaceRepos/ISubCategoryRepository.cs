using Domain.Entities;

namespace Application.InterfaceRepos;

public interface ISubCategoryRepository
{
    List<SubCategory> GetAllSubCategoriesFromCategory(int categoryId);
    List<SubCategory> GetAllSubCategories();
    SubCategory addSubCategoryToCategory(SubCategory dto);
    SubCategory deleteSubCategoryFromCategory( int subcategoryId);

    SubCategory updateSubCategory(int id, SubCategory subCategory);
}