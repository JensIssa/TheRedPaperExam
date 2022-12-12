using Application.DTOs;
using Domain.Entities;

namespace Application.InterfaceServices;

public interface ISubCategoryService
{
    List<SubCategory> GetAllSubCategoriesFromCategory(int categoryId);
    List<SubCategory> GetAllSubCategories();
    SubCategory AddSubCategoryToCategory( PostSubCategoryDTO dto);
    SubCategory DeleteSubCategoryFromCategory(int subcategoryId);

    SubCategory UpdateSubCategory(int id, PutSubCategoryDTO dto);
    
}