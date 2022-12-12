using Domain.Entities;

namespace Application.InterfaceRepos;

public interface ISubCategoryRepository
{
    List<SubCategory> GetAllSubCategoriesFromCategory(int categoryId);
    List<SubCategory> GetAllSubCategories();
    SubCategory AddSubCategoryToCategory(SubCategory dto);
    SubCategory DeleteSubCategoryFromCategory( int subcategoryId);

    SubCategory UpdateSubCategory(int id, SubCategory subCategory);
}