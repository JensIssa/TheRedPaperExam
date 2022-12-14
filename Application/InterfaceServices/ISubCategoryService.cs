using Application.DTOs;
using Domain.Entities;

namespace Application.InterfaceServices;

public interface ISubCategoryService
{
    /// <summary>
    /// This method returns a list of all the subcategories connected to a specific category
    /// </summary>
    /// <param name="categoryId">The id of the specific category the subcategories are connected to</param>
    /// <returns>returns a list of all the subcategories connected to a specific category</returns>
    List<SubCategory> GetAllSubCategoriesFromCategory(int categoryId);
    /// <summary>
    /// This method returns a list of all the subcategories
    /// </summary>
    /// <returns>a list of all the subcategories</returns>
    List<SubCategory> GetAllSubCategories();
    /// <summary>
    /// This method adds a subcategory to a specific category
    /// </summary>
    /// <param name="dto">The dto contains all the properties used to add a subcategory to a specific category</param>
    /// <returns>The subcategory now added to a specific category</returns>
    SubCategory AddSubCategoryToCategory( PostSubCategoryDTO dto);
    /// <summary>
    /// This method deletes a subcategory connected to a specific category
    /// </summary>
    /// <param name="subcategoryId">the id of the subcategory which is getting deleted</param>
    /// <returns>the deleted subcategory</returns>
    SubCategory DeleteSubCategoryFromCategory(int subcategoryId);

    /// <summary>
    /// This method updates a specific subcategory
    /// </summary>
    /// <param name="id">The id of the subcategory which is getting updated</param>
    /// <param name="dto">This dto contains the properties used to update the subcategory</param>
    /// <returns>an updated subcategory</returns>
    SubCategory UpdateSubCategory(int id, PutSubCategoryDTO dto);
    
}