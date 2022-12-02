using Application.InterfaceRepos;
using Application.InterfaceServices;
using Domain.Entities;

namespace Application.Services;

public class ConditionService : IConditionService
{
    public IConditionRepository _repository;

    public ConditionService(IConditionRepository repository)
    {
        _repository = repository;
    }

    public List<Condition> getAllConditions()
    {
        return _repository.getConditions();
    }
}