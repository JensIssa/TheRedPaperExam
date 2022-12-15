using Application.DTOs;
using Application.InterfaceRepos;
using Application.InterfaceServices;
using AutoMapper;
using Domain.Entities;
using FluentValidation;

namespace Application.Services;

public class SubCategoryService : ISubCategoryService
{

    private readonly ISubCategoryRepository _repository;
    private readonly IMapper _imapper;
    private readonly IValidator<PostSubCategoryDTO> _postDTO;
    private readonly IValidator<PutSubCategoryDTO> _putDTO;

    public SubCategoryService(ISubCategoryRepository repository, IMapper imapper, IValidator<PostSubCategoryDTO> postDto, IValidator<PutSubCategoryDTO> putDto)
    {
        _repository = repository;
        _imapper = imapper;
        _postDTO = postDto;
        _putDTO = putDto;
        ExceptionHandlingConstructor();
    }

    public List<SubCategory> GetAllSubCategoriesFromCategory(int categoryId)
    {
        return _repository.GetAllSubCategoriesFromCategory(categoryId); 
    }

    public List<SubCategory> GetAllSubCategories()
    {
        return _repository.GetAllSubCategories();
    }

    public SubCategory AddSubCategoryToCategory( PostSubCategoryDTO dto)
    {
        var validation = _postDTO.Validate(dto);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }
        return _repository.AddSubCategoryToCategory(_imapper.Map<SubCategory>(dto));
    }
    
    public SubCategory DeleteSubCategoryFromCategory(int subcategoryId)
    {
        if (subcategoryId==null|| subcategoryId<1)
        {
            throw new ArgumentException("The SubCategory id is not found");
        }
        return _repository.DeleteSubCategoryFromCategory(subcategoryId);
    }

    public SubCategory UpdateSubCategory(int id, PutSubCategoryDTO dto)
    {
        var validation = _putDTO.Validate(dto);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }
        return _repository.UpdateSubCategory(id, _imapper.Map<SubCategory>(dto));    
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