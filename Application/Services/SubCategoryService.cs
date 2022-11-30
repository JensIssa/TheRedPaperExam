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
    }

    public List<SubCategory> GetAllSubCategoriesFromCategory(int categoryId)
    {
        return _repository.GetAllSubCategoriesFromCategory(categoryId); 
    }

    public SubCategory addSubCategoryToCategory( PostSubCategoryDTO dto)
    {
        ExceptionHandlingCreate(dto);
        var validation = _postDTO.Validate(dto);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }
        return _repository.addSubCategoryToCategory(_imapper.Map<SubCategory>(dto));
    }
    
    public SubCategory deleteSubCategoryFromCategory(int subcategoryId)
    {
        if (subcategoryId==null|| subcategoryId<1)
        {
            throw new ArgumentException("The SubCategory id is not found");
        }
        return _repository.deleteSubCategoryFromCategory(subcategoryId);
    }

    public SubCategory updateSubCategory(int id, PutSubCategoryDTO dto)
    {
        ExceptionHandlingUpdate(dto);
        var validation = _putDTO.Validate(dto);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }
        return _repository.updateSubCategory(id, _imapper.Map<SubCategory>(dto));    
    }


    private void ExceptionHandlingCreate(PostSubCategoryDTO dto)
    {
        if (string.IsNullOrEmpty(dto.SubName))
        {
            throw new ArgumentException("This SubCategory name is empty/null");
        }

        if (dto.CategoryID == null)
        {
            throw new ArgumentException("This subcategory needs to be linked with a Category");
        }
    }
    
    private void ExceptionHandlingUpdate(PutSubCategoryDTO dto)
    {
        if (string.IsNullOrEmpty(dto.SubName))
        {
            throw new ArgumentException("This SubCategory name is empty/null");
        }

        if (dto.CategoryID == null || dto.CategoryID < 1)
        {
            throw new ArgumentException("This subcategory needs to be linked with a Category");
        }
        if (dto.Id == null || dto.Id < 1)
        {
            throw new ArgumentException("This subcategroy is not found");
        }
    }
}