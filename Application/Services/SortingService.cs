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
        return _sortingRepository.SortProductsAlphabeticallyBySubId(subcategoryId).OrderBy(p => p.ProductName).Reverse().ToList();
    }

    public List<Product> SortProductsAlphabeticallyReverseBySubId(int subCategoryId)
    {
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
        return _sortingRepository.SortProductsByPriceBySubId(subCategoryId).OrderBy(p => p.Price).ToList();
    }

    public List<Product> SortProductsByPriceReverseBySubId(int subCategoryId)
    {
        return _sortingRepository.SortProductsByPriceBySubId(subCategoryId).OrderBy(p => p.Price).Reverse().ToList();
    }
}