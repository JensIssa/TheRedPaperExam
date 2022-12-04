using Domain.Entities;

namespace Application.InterfaceRepos;

public interface ISortingRepository
{
    List<Product> SortProductsAlphabetically();

    List<Product> SortProductsAlphabeticallyReverse();
    
    List<Product> SortProductsAlphabeticallyBySubId(int subcategoryId);

    List<Product> SortProductsAlphabeticallyReverseBySubId(int subCategoryId);

    List<Product> SortProductsByPrice();

    List<Product> SortProductsByPriceReverse();
     
    List<Product> SortProductsByPriceBySubId(int subCategoryId);

    List<Product> SortProductsByPriceReverseBySubId(int subCategoryId);
}