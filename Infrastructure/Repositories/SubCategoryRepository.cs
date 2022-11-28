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
        return _context.SubCategoryTable.Where(i => i.CategoryID.Equals(categoryId)).Include((x) => x.Category).ToList();
    }

    public SubCategory addSubCategoryToCategory(SubCategory dto)
    {
        _context.Add(dto); 
        _context.SaveChanges();
        return dto;
    }

    public SubCategory deleteSubCategoryFromCategory(int categoryId, int subcategoryId)
    {
        throw new NotImplementedException();
    }
}