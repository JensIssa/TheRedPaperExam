using Application.DTOs;
using Domain.Entities;

namespace Application.InterfaceServices;

public interface ICategoryService
{
    public List<Category> GetAllCategories();
    public Category CreateCategory(PostCategoryDTO dto);
    public Category UpdateCategory(int id, PutCategoryDTO dto);
    public Category DeleteCategory(int id);


}