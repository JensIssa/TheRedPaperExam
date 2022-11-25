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
        throw new NotImplementedException();
    }

    public Category CreateCategory(PostCategoryDTO dto)
    {
        throw new NotImplementedException();
    }

    public Category UpdateCategory(int id, PutCategoryDTO dto)
    {
        throw new NotImplementedException();
    }

    public Category DeleteCategory(int id)
    {
        throw new NotImplementedException();
    }
}