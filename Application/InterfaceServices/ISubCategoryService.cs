using Application.DTOs;
using Domain.Entities;

namespace Application.InterfaceServices;

public interface ISubCategoryService
{
    List<SubCategory> GetAllSubCategoriesFromCategory(int categoryId);
    SubCategory addSubCategoryToCategory( PostSubCategoryDTO dto);
    SubCategory deleteSubCategoryFromCategory(int subcategoryId);


}