using Application.DTOs;
using Domain.Entities;

namespace Application.InterfaceServices;

public interface ICategoryService
{
     
     /// <summary>
     /// This method gets all the category objects in a list
     /// </summary>
     /// <returns>returns a list of categories</returns>
     List<Category> GetAllCategories();
     /// <summary>
     /// This method creates a category
     /// </summary>
     /// <param name="dto">this dto contains the properties that are used to create a category</param>
     /// <returns>the created category</returns>
     Category CreateCategory(PostCategoryDTO dto);
     /// <summary>
     /// This method updates a category object
     /// </summary>
     /// <param name="id">the specific id of the specific category which is getting an update</param>
     /// <param name="dto">this dto contains the properties that are used to update a category</param>
     /// <returns>returns the updated category</returns>
     Category UpdateCategory(int id, PutCategoryDTO dto);
     /// <summary>
     /// This method deletes a category object
     /// </summary>
     /// <param name="id">the specific id of the specific category which is getting deleted</param>
     /// <returns>the deleted category</returns>
     Category DeleteCategory(int id);


}