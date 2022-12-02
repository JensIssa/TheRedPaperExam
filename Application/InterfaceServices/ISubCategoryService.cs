using Application.DTOs;
using Domain.Entities;

namespace Application.InterfaceServices;

public interface ISubCategoryService
{
    List<SubCategory> GetAllSubCategoriesFromCategory(int categoryId);
    List<SubCategory> getAllSubCategories();
    SubCategory addSubCategoryToCategory( PostSubCategoryDTO dto);
    SubCategory deleteSubCategoryFromCategory(int subcategoryId);

    SubCategory updateSubCategory(int id, PutSubCategoryDTO dto);
    
}