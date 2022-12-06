using Application.InterfaceRepos;
using Application.InterfaceServices;
using Domain.Entities;

namespace Application.Services;

public class SortingService : ISortingService
{
    private ISortingRepository _sortingRepository;

    public SortingService(ISortingRepository sortingRepository)
    {
        _sortingRepository = sortingRepository;
    }

    public List<Product> SortProductsAlphabetically()
    {
        return _sortingRepository.SortProductsAlphabetically().OrderBy(p => p.ProductName).ToList();
    }

    public List<Product> SortProductsAlphabeticallyReverse()
    {
        return _sortingRepository.SortProductsAlphabeticallyReverse().OrderBy(p => p.ProductName).Reverse().ToList();
    }

    public List<Product> SortProductsAlphabeticallyBySubId(int subcategoryId)
    {
        if (subcategoryId == null || subcategoryId < 1)
        {
            throw new ArgumentException("There is no Subcategory to view products from");
        }
        return _sortingRepository.SortProductsAlphabeticallyBySubId(subcategoryId).OrderBy(p => p.ProductName).Reverse().ToList();
    }

    public List<Product> SortProductsAlphabeticallyReverseBySubId(int subCategoryId)
    {
        if (subCategoryId == null || subCategoryId < 1)
        {
            throw new ArgumentException("There is no Subcategory to view products from");
        }
        return _sortingRepository.SortProductsAlphabeticallyReverseBySubId(subCategoryId).OrderBy(p => p.ProductName).Reverse().ToList();
    }

    public List<Product> SortProductsByPrice()
    {
        return _sortingRepository.SortProductsByPrice().OrderBy(p => p.Price).ToList();
    }

    public List<Product> SortProductsByPriceReverse()
    {
        return _sortingRepository.SortProductsByPriceReverse().OrderBy(p => p.Price).Reverse().ToList();
    }

    public List<Product> SortProductsByPriceBySubId(int subCategoryId)
    {
        if (subCategoryId == null || subCategoryId < 1)
        {
            throw new ArgumentException("There is no Subcategory to view products from");
        }
        return _sortingRepository.SortProductsByPriceBySubId(subCategoryId).OrderBy(p => p.Price).ToList();
    }

    public List<Product> SortProductsByPriceReverseBySubId(int subCategoryId)
    {
        if (subCategoryId == null || subCategoryId < 1)
        {
            throw new ArgumentException("There is no Subcategory to view products from");
        }
        return _sortingRepository.SortProductsByPriceBySubId(subCategoryId).OrderBy(p => p.Price).Reverse().ToList();
    }
}