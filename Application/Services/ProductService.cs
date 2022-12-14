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
        ExceptionHandlingConstructor();
    }

    public List<Product> GetAllProducts()
    {
        return _repository.GetAllProducts();
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
        var validation = _postDTO.Validate(dto);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }

        return _repository.AddProductToUser(_imapper.Map<Product>(dto));
    }
    
    public Product DeleteProductFromUser(int id)
    {
        if (id.Equals(null)|| id<1)
        {
            throw new ArgumentException("The Product id is not found");
        }
        return _repository.DeleteProductFromUser(id);
    }

    public Product UpdateProduct(int productId, PutProductDTO dto)
    {
        var validation = _putDTO.Validate(dto);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }
        return _repository.UpdateProduct(productId, _imapper.Map<Product>(dto));      }


    public List<Product> GetProductsByOrderId(int orderId)
    {
        return _repository.GetProductsByOrderId(orderId);
    }
    
    public void ExceptionHandlingConstructor()
    {
        if (_repository == null)
        {
            throw new ArgumentException("This service cannot be constructed without a repository");
        }

        if (_imapper == null)
        {
            throw new ArgumentException("This service cannot be constructed without a mapper");
        }

        if (_putDTO == null )
        {
            throw new ArgumentException("This service cannot be constructed without a putValidator");
        }

        if (_postDTO == null)
        {
            throw new ArgumentException("This service cannot be constructed without a postValidator");
        }
    }
}