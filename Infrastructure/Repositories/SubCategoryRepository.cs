using Application.InterfaceRepos;
using Domain.Entities;

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
        var idToExtractFrom = _context.CategoryTable.Find(categoryId) ??
                              throw new KeyNotFoundException("Id to update not found");
        return _context.SubCategoryTable.Where(i => i.CategoryID.Equals(idToExtractFrom)).ToList();
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