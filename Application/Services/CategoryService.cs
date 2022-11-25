using Application.DTOs;
using Application.InterfaceRepos;
using Application.InterfaceServices;
using AutoMapper;
using Domain.Entities;
using FluentValidation;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<PutCategoryDTO> _putValidator;
    private readonly IValidator<PostCategoryDTO> _postValidator;

    public CategoryService(ICategoryRepository repository, IMapper mapper, IValidator<PutCategoryDTO> putValidator, IValidator<PostCategoryDTO> postValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _putValidator = putValidator;
        _postValidator = postValidator;
    }

    public List<Category> GetAllCategories()
    {
        return _repository.GetAllCategories();
    }

    public Category CreateCategory(PostCategoryDTO dto)
    {
        ExceptionHandlingCreate(dto);
        var validation = _postValidator.Validate(dto);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }

        return _repository.CreateCategory(_mapper.Map<Category>(dto));
    }

    public Category UpdateCategory(int id, PutCategoryDTO dto)
    {
        ExceptionHandlingUpdate(dto);
        var validation = _putValidator.Validate(dto);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }

        return _repository.UpdateCategory(id, _mapper.Map<Category>(dto));
    }

    public Category DeleteCategory(int id)
    {
        if (id==null|| id<1)
        {
            throw new ArgumentException("The category is not found");
        }
        return _repository.DeleteCategory(id);
    }

    private void ExceptionHandlingCreate(PostCategoryDTO dto)
    {
        if (string.IsNullOrEmpty(dto.CategoryName))
        {
            throw new ArgumentException("This category name is empty/null");
        }
    }

    private void ExceptionHandlingUpdate(PutCategoryDTO dto)
    {
        if (dto.Id==null|| dto.Id<1)
        {
            throw new ArgumentException("The category Id is null/<1");
        }
        if (string.IsNullOrEmpty(dto.CategoryName))
        {
            throw new ArgumentException("This category name is empty/null");
        }
    }
}