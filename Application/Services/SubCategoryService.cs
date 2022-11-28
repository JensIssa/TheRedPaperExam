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
        var validation = _postDTO.Validate(dto);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }
        return _repository.addSubCategoryToCategory(_imapper.Map<SubCategory>(dto));
    }

    public SubCategory addSubCategoryToCategory(int categoryId, int subcategoryId)
    {
        throw new NotImplementedException();
    }

    public SubCategory deleteSubCategoryFromCategory(int categoryId, int subcategoryId)
    {
        throw new NotImplementedException();
    }
}