using Domain.Entities;

namespace Application.InterfaceRepos;

public interface ICategoryRepository
{
    /// <summary>
    /// This method returns a list of all the category objects from the database
    /// </summary>
    /// <returns>a list of category objects</returns>
    List<Category> GetAllCategories();
    /// <summary>
    /// This method creates a category object to the database
    /// </summary>
    /// <param name="category">The category object</param>
    /// <returns>The created category object</returns>
    Category CreateCategory(Category category);
    /// <summary>
    /// This method updates a category object in the database
    /// </summary>
    /// <param name="id">the id of the specific category object to be updated</param>
    /// <param name="category">The category object</param>
    /// <returns></returns>
    Category UpdateCategory(int id, Category category);
    /// <summary>
    /// This method deletes a specific category object from the database
    /// </summary>
    /// <param name="id">the id of the specific category to be deleted</param>
    /// <returns>the deleted category object</returns>
    Category DeleteCategory(int id);
}