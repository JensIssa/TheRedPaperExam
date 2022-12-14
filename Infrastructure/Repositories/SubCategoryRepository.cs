using Application.InterfaceRepos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SubCategoryRepository : ISubCategoryRepository
{
    private  RepositoryDBContext _context;

    public SubCategoryRepository(RepositoryDBContext context)
    {
        _context = context;
    }

    public List<SubCategory> GetAllSubCategoriesFromCategory(int categoryId)
    {
        return _context.SubCategoryTable.Where(i => i.CategoryID == categoryId).ToList();
    }

    public List<SubCategory> GetAllSubCategories()
    {
        return _context.SubCategoryTable.ToList();
    }

    public SubCategory AddSubCategoryToCategory(SubCategory dto)
    {
        _context.Add(dto); 
        _context.SaveChanges();
        return dto;
    }

    public SubCategory DeleteSubCategoryFromCategory(int subcategoryId)
    {
        var subCategoryToDelete = _context.SubCategoryTable.Find(subcategoryId) ?? throw new KeyNotFoundException("Id not found");
        _context.SubCategoryTable.Remove(subCategoryToDelete);
        _context.SaveChanges();
        return subCategoryToDelete;
    }

    public SubCategory UpdateSubCategory(int id, SubCategory subCategory)
    {
        var subCategoryToUpdate = _context.SubCategoryTable.Find(id) ?? throw new KeyNotFoundException("Id to update not found");
        _context.SubCategoryTable.Update(subCategoryToUpdate);
        _context.SaveChanges();
        return subCategoryToUpdate;
    }
}