using Domain.Entities;

namespace Application.InterfaceRepos;

public interface ISubCategoryRepository
{
    /// <summary>
    /// This method gets all the subcategories connected to a specific category from the database
    /// </summary>
    /// <param name="categoryId">The id of the specific category</param>
    /// <returns>Returns a list of all categories connected to a specific category</returns>
    List<SubCategory> GetAllSubCategoriesFromCategory(int categoryId);
    /// <summary>
    /// This method returns a list of all the subcategories from the database
    /// </summary>
    /// <returns>A list of all the subcategories</returns>
    List<SubCategory> GetAllSubCategories();
    /// <summary>
    /// This method creates a subcategory to a specific category to the database 
    /// </summary>
    /// <param name="dto">The dto containing all the paramaters to create a new subcategory to a category</param>
    /// <returns>A new subcategory</returns>
    SubCategory AddSubCategoryToCategory(SubCategory dto);
    /// <summary>
    /// This method deletes a subcategory from the database
    /// </summary>
    /// <param name="subcategoryId">Id of the subcategory to be deleted</param>
    /// <returns>The subcategory is deleted from the database</returns>
    SubCategory DeleteSubCategoryFromCategory( int subcategoryId);
    /// <summary>
    /// This method updates a subcategory in the database
    /// </summary>
    /// <param name="id">The id of the subcategory to be updated</param>
    /// <param name="subCategory">the subcategory object</param>
    /// <returns>An updated subcategory in the database</returns>
    SubCategory UpdateSubCategory(int id, SubCategory subCategory);
}