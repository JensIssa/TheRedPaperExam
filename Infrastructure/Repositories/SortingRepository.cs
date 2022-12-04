using Application.InterfaceRepos;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class SortingRepository : ISortingRepository
{
    private RepositoryDBContext _repositoryDbContext;

    public SortingRepository(RepositoryDBContext repositoryDbContext)
    {
        _repositoryDbContext = repositoryDbContext;
    }

    public List<Product> SortProductsAlphabetically()
    {
        return _repositoryDbContext.ProductTable.ToList();
    }

    public List<Product> SortProductsAlphabeticallyReverse()
    {
        return _repositoryDbContext.ProductTable.ToList();
    }

    public List<Product> SortProductsAlphabeticallyBySubId(int subcategoryId)
    {
        return _repositoryDbContext.ProductTable.Where(p => p.SubCategoryID == subcategoryId).ToList();
    }

    public List<Product> SortProductsAlphabeticallyReverseBySubId(int subCategoryId)
    {
        return _repositoryDbContext.ProductTable.Where(p => p.SubCategoryID == subCategoryId).ToList();
    }

    public List<Product> SortProductsByPrice()
    {
        return _repositoryDbContext.ProductTable.ToList();
    }

    public List<Product> SortProductsByPriceReverse()
    {
        return _repositoryDbContext.ProductTable.ToList();
    }

    public List<Product> SortProductsByPriceBySubId(int subCategoryId)
    {
        return _repositoryDbContext.ProductTable.Where(p => p.SubCategoryID == subCategoryId).ToList();
    }

    public List<Product> SortProductsByPriceReverseBySubId(int subCategoryId)
    {
        return _repositoryDbContext.ProductTable.Where(p => p.SubCategoryID == subCategoryId).ToList();
    }
}