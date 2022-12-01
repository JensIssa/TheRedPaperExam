using Application.DTOs;
using Application.InterfaceRepos;
using Application.InterfaceServices;
using AutoMapper;
using Domain.Entities;
using FluentValidation;

namespace Application.Services;

public class ProductService : IProductService
{

    private readonly IProductRepository _repository;
    private readonly IMapper _imapper;
    private readonly IValidator<PostProductDTO> _postDTO;
    private readonly IValidator<PutProductDTO> _putDTO;

    public ProductService(IProductRepository repository, IMapper imapper, IValidator<PostProductDTO> postDto, IValidator<PutProductDTO> putDto)
    {
        _repository = repository;
        _imapper = imapper;
        _postDTO = postDto;
        _putDTO = putDto;
    }

    public List<Product> GetAllProductsFromSubcategory(int subcategoryId)
    {
        return _repository.GetAllProductsFromSubcategory(subcategoryId);
    }

    public List<Product> GetAllProductsFromUser(int userId)
    {
        return _repository.GetAllProductsFromUser(userId);
    }

    public Product AddProductToUser(PostProductDTO dto)
    {
        ExceptionHandlingCreate(dto);
        var validation = _postDTO.Validate(dto);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }

        return _repository.AddProductToUser(_imapper.Map<Product>(dto));
    }

    public List<Condition> conditionList()
    {
        throw new NotImplementedException();
    }

    public void ExceptionHandlingCreate(PostProductDTO dto)
    {
        if (string.IsNullOrEmpty(dto.Description))
        {
            throw new ArgumentException("The description is empty or null");
        }

        if (string.IsNullOrEmpty(dto.ImageUrl))
        {
            throw new ArgumentException("The imageUrl is empty or null");
        }
        
        if (string.IsNullOrEmpty(dto.ProductName))
        {
            throw new ArgumentException("The productName is empty or null");
        }

        if (dto.UserId == null || dto.UserId < 1)
        {
            throw new ArgumentException("The userID is null / <1");
        }
        
        if (dto.SubCategoryID == null || dto.SubCategoryID < 1)
        {
            throw new ArgumentException("The subCategoryId is null / <1");
        }

        if (dto.Price == null || dto.Price < 1)
        {
            throw new ArgumentException("The price is null / <1");
        }
    }
    public Product DeleteProductFromUser(int productId)
    {
        if (productId==null|| productId<1)
        {
            throw new ArgumentException("The Product id is not found");
        }
        return _repository.DeleteProductFromUser(productId);
    }

    public Product UpdateProduct(int productId, PutProductDTO dto)
    {
        throw new NotImplementedException();
    }

    public Product getProductById(int productID)
    {
        if (productID == null || productID < 1)
        {
            throw new ArgumentException("id is null or < 1");
        }
        return _repository.getProductById(productID);    }
}