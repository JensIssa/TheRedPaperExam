using Application.DTOs;
using Domain.Entities;

namespace Application.InterfaceServices;

public interface ICategoryService
{
     List<Category> GetAllCategories();
     Category CreateCategory(PostCategoryDTO dto);
     Category UpdateCategory(int id, PutCategoryDTO dto);
     Category DeleteCategory(int id);


}